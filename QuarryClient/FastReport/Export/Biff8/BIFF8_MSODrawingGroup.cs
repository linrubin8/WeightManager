using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;


namespace FastReport.Export.BIFF8
{

    internal class BIFF8_MSODrawingRecord
    {
        Byte        btWin32;
        Byte        btMacOS;
        Byte[]      UID;
        UInt16      tag;
        UInt32      size;
        UInt32      ref_count;
        UInt32      file_offset;
        Byte        usage;
        Byte        cbName;

        UInt16      picture_type;
        UInt16      blip_packed;
//        Byte[]      UID0;
        Byte[]      pic;
        Byte        image_tag;

        ushort packed_options;

        internal void AddReference() { ref_count++; }

        internal void Read(StreamHelper stream, ushort packed_options)
        {
            btWin32 = (Byte)stream.ReadByte();
            btMacOS = (Byte)stream.ReadByte();
            UID = stream.ReadBytes(16);
            tag = stream.ReadUshort();
            size = stream.ReadUint();
            ref_count = stream.ReadUint();
            file_offset = stream.ReadUint();
            usage = (Byte)stream.ReadByte();
            cbName = (Byte)stream.ReadByte();
            stream.SkipBytes(2);

            this.packed_options = packed_options;
        }

        internal void Read_Picture(StreamHelper stream, UInt16 blip_packed, UInt16 type, int picture_size)
        {
            // picture_size = (int)blip_record_size - 17;

            Byte[]      UID0;
            UID0 = stream.ReadBytes(16);
            //// Byte[] UID1 = ReadBytes(16);
            image_tag = (Byte)stream.ReadByte();
            pic = stream.ReadBytes(picture_size);
            this.picture_type = type;
            this.blip_packed = blip_packed;

            //FileStream f = new FileStream(@"C:\Users\alman\Documents\pict.png", FileMode.Create);
            //f.Write(pic, 0, pic.Length);
            //f.Close();
        }

        internal uint Write_Picture(StreamHelper stream)
        {
            uint payload_size = (uint) (UID.Length + 1 + pic.Length);

            stream.WriteUshort(this.blip_packed); // 0x6e00
            stream.WriteUshort(this.picture_type);     // PNG = 0xf01e
            stream.WriteUint(payload_size);

            stream.WriteBytes(UID);
            //// stream.WriteBytes(UID1);
            stream.WriteByte(image_tag);
            stream.WriteBytes(pic);

            //FileStream f = new FileStream(@"C:\Users\alman\Documents\pict.png", FileMode.Create);
            //f.Write(pic, 0, pic.Length);
            //f.Close();

            return payload_size + 8;
        }

        internal void Write(StreamHelper stream)
        {
            stream.WriteUshort(packed_options);  // 0x0052
            stream.WriteUshort(0xf007);     // BSE
            long bse_size_position = stream.Position;
            stream.WriteUint(0x0000);

            stream.WriteByte(btWin32);
            stream.WriteByte(btMacOS);
            stream.WriteBytes(UID);
            stream.WriteUshort(tag);
            stream.WriteUint(size);
            stream.WriteUint(ref_count);
            stream.WriteUint(file_offset);
            stream.WriteByte(usage);
            stream.WriteByte(cbName);
            stream.SkipBytes(2);

            uint blip_size = Write_Picture(stream);

            long current_position = stream.Position;
            blip_size += 36;
            stream.Position = bse_size_position;
            stream.WriteUint((UInt32)blip_size);
            stream.Position = current_position;
        }

        internal void LoadPicture(FastReport.Export.ExportIEMObject Obj, int index)
        {
            btWin32 = 6;  // PNG
            btMacOS = 6;  // PNG
            UID = new byte[16];
            tag = 0xff;
            size = (uint) (8 + 16 + 1 + Obj.PictureStream.Length);
            ref_count = 1;
            file_offset = 0;
            usage = 0;
            cbName = 0;

            pic = new byte[Obj.PictureStream.Length];
            Obj.PictureStream.Read(pic, 0, (int)Obj.PictureStream.Length);

            // Take midddle bytes as hash
            int idx = pic.Length/2;
            for (int i = 0; i < 16; i++)
                UID[i] = pic[i + idx];
            UID[0] = (byte)index;
            UID[15] = (byte)(index >> 4);

            picture_type = 0xf01e;
            blip_packed = 0x6e00;
            image_tag = 0xff;

            packed_options = 0x62;
        }
    }

    class BIFF8_MSODrawingGroup : ArrayList
    {
        UInt32 mso_spid;
        UInt32 cidcl;
        UInt32 cspSaved;
        UInt32 cdgSaved;

        ArrayList prop_list = new ArrayList();

        UInt32 FillColor;
        UInt32 LineColor;
        UInt32 ShadowColor;
        UInt32 ThreeDColor;

        private void Read_BstoreContainer(StreamHelper stream, UInt32 loop_record_size)
        {
            BIFF8_MSODrawingRecord local_record = new BIFF8_MSODrawingRecord();

            while (loop_record_size > 0)
            {
                UInt16 blip_packed_data = stream.ReadUshort();
                UInt16 blip_fbt = stream.ReadUshort();
                UInt32 blip_record_size = stream.ReadUint();

                // Get BLIP data
                switch (blip_fbt)
                {
                    case 0xf007: // msofbtBSE
                        local_record = new BIFF8_MSODrawingRecord();
                        local_record.Read(stream, blip_packed_data);
                        this.Add(local_record);
                        loop_record_size -= 36 /*blip_record_size*/;
                        break;

                    case 0xf01e: // PNG
                    case 0xf01d: // JPEG
                        local_record.Read_Picture(stream, blip_packed_data, blip_fbt, (int)blip_record_size - 17);
                        loop_record_size -= blip_record_size;
                        break;

                    default:
                        throw new Exception("BLIP not parsed yet");
                }
                loop_record_size -= 8; //HEADER SIZE
            }
        }

        internal int Read(StreamHelper stream, int RecordSize)
        {
            long stored_position = stream.Position;

            UInt16 packed_data = stream.ReadUshort();
            UInt16 fbt = stream.ReadUshort();
            UInt32 total_record_size = stream.ReadUint();

            RecordSize -= sizeof(UInt16) + sizeof(UInt16) + sizeof(UInt32);

            if (RecordSize > total_record_size)
            {
                throw new Exception("MSODRAWINGGROUP: Data layout error");
            }

            if (RecordSize < total_record_size)
            {
                StreamHelper virtual_stream = new StreamHelper();

                int tail = (int)total_record_size;
                do
                {
                    byte[] record = stream.ReadBytes( RecordSize);
                    virtual_stream.WriteBytes(record);
                    //stream.Position += RecordSize;
                    tail -= RecordSize;

                    if (tail == 0) break;

                    UInt16 RecordID = stream.ReadUshort();
                    if ( RecordID != 0x0003c && RecordID != 0x000eb ) // CONTINUE record
                    {
                        throw new Exception("MSODRAWINGGROUP: Data layout error");
                    }
                    RecordSize = stream.ReadUshort();
                }
                while (tail > 0);

                RecordSize = (int)(stream.Position - stored_position);
                stream = virtual_stream;
                stream.Position = 0;
            }
            else
            {
                RecordSize += sizeof(UInt16) + sizeof(UInt16) + sizeof(UInt32);
            }

            while (total_record_size > 0)
            {

                UInt16 dgg_packed_data = stream.ReadUshort();
                UInt16 dgg_fbt = stream.ReadUshort();
                UInt32 dgg_record_size = stream.ReadUint();

                long shift = stream.Position - stored_position;

                switch (dgg_fbt)
                {
                    case 0xf001: // msofbtBstoreContainer
                        this.Read_BstoreContainer(stream, dgg_record_size);
                        break;

                    case 0xf006: // msofbtDgg
                        mso_spid = stream.ReadUint();
                        cidcl = stream.ReadUint();
                        cspSaved = stream.ReadUint();
                        cdgSaved = stream.ReadUint();
                        for (int cid_idx = 1; cid_idx < cidcl; cid_idx++)
                        {
                            UInt32 dgid = stream.ReadUint();
                            UInt32 cspidCur = stream.ReadUint();
                        }
                        break;

                    case 0xf00b: // msofbtOPT
                        UInt32 local_size = dgg_record_size;

                        while (local_size > 0)
                        {
                            BIFF8_ShapeProp prop = new BIFF8_ShapeProp();
                            UInt32 prop_size = prop.ReadProperty(stream);
                            prop_list.Add(prop);
                            local_size -= prop_size;
                        }

                        foreach (BIFF8_ShapeProp prop in prop_list)
                        {
                            prop.ReadPayload(stream);
                        }
                        break;

                    case 0xf11e: // msofbtSplitMenuColors
                        FillColor = stream.ReadUint();
                        LineColor = stream.ReadUint();
                        ShadowColor = stream.ReadUint();
                        ThreeDColor = stream.ReadUint();
                        break;

                    default:
                        stream.SkipBytes((int)dgg_record_size);
                        break;
                }

                total_record_size -= 8 + dgg_record_size;
            }

            return RecordSize;
        }

        internal void Write(StreamHelper stream)
        {
            StreamHelper helper_stream = new StreamHelper();

            this.mso_spid = 1026;
            this.cidcl = 2;
            this.cspSaved = 2;
            this.cdgSaved = (uint) this.Count;

            prop_list.Add(new BIFF8_ShapeProp(191, 0x00080008));
            prop_list.Add(new BIFF8_ShapeProp(385, 0x08000041));
            prop_list.Add(new BIFF8_ShapeProp(448, 0x08000040));

            helper_stream.WriteUshort(0x000f);
            helper_stream.WriteUshort(0xf000); // msofbtDggContainer
            long dgg_container_size_position = helper_stream.Position;
            helper_stream.WriteUint(0); // fix me

            helper_stream.WriteUshort(0x0000);
            helper_stream.WriteUshort(0xf006);  // msofbtDgg
            uint dgg_record_size = 16 + (cidcl - 1) * 8;
            helper_stream.WriteUint(dgg_record_size);  // dgg_record_size
            helper_stream.WriteUint(mso_spid);
            helper_stream.WriteUint(cidcl);
            helper_stream.WriteUint(cspSaved);
            helper_stream.WriteUint(cdgSaved);
            for (int cid_idx = 1; cid_idx < cidcl; cid_idx++)
            {
                UInt32 dgid = 1; // fix me
                UInt32 cspidCur = 2; // fix me
                helper_stream.WriteUint(dgid);
                helper_stream.WriteUint(cspidCur);
            }

            helper_stream.WriteUshort((ushort)(0x000f | (this.Count << 4)));
            helper_stream.WriteUshort(0xf001);     // msofbtBstoreContainer
            long bstore_size_position = helper_stream.Position;
            helper_stream.WriteUint(0x0000);

            foreach (BIFF8_MSODrawingRecord record in this)
            {
                record.Write(helper_stream);
            }

            long current_stream_position = helper_stream.Position;
            helper_stream.Position = bstore_size_position;
            bstore_size_position = current_stream_position - bstore_size_position - 4;
            helper_stream.WriteUint((uint)bstore_size_position);
            helper_stream.Position = current_stream_position;

            helper_stream.WriteUshort(0x0033);
            helper_stream.WriteUshort(0xf00b);      // msofbtOPT
            long opt_size_position = helper_stream.Position;
            helper_stream.WriteUint(0x0000);
            foreach (BIFF8_ShapeProp prop in prop_list) prop.WriteProperty(helper_stream);
            foreach (BIFF8_ShapeProp prop in prop_list) prop.WritePayload(helper_stream);
            // Fix options size
            long curr_position = helper_stream.Position;
            helper_stream.Position = opt_size_position;
            opt_size_position = curr_position - opt_size_position - sizeof(UInt32);
            helper_stream.WriteUint((ushort)opt_size_position);
            helper_stream.Position = curr_position;

            // skip SplitMenuColors
            helper_stream.WriteUshort(0x0040);
            helper_stream.WriteUshort(0xf11e); // msofbtSplitMenuColors
            helper_stream.WriteUint(16);
            helper_stream.WriteUint(FillColor);
            helper_stream.WriteUint(LineColor);
            helper_stream.WriteUint(ShadowColor);
            helper_stream.WriteUint(ThreeDColor);

            long last_byte_position = helper_stream.Position;

            helper_stream.Position = dgg_container_size_position;
            dgg_container_size_position = last_byte_position - dgg_container_size_position - 4;
            helper_stream.WriteUint((uint)dgg_container_size_position);

            helper_stream.Position = 0;

            // Split huge temporary stream into records

            int iteration_trick = 0;
            int tail_size = (int) helper_stream.Length;
            ushort chunk_size;
            
            while( tail_size > 0 )
            {
                ushort item_type = (ushort) ((iteration_trick < 2) ? 0x00eb : 0x003c);
                stream.WriteUshort(item_type); // MSODRAWINGGROUP : CONTINUE
                chunk_size = (ushort)((tail_size <= 8224) ? tail_size : 8224);
                stream.WriteUshort(chunk_size);

                byte[] pictures = helper_stream.ReadBytes(chunk_size);
                stream.WriteBytes(pictures);

                tail_size -= chunk_size;
                iteration_trick++;
            }

            helper_stream.Dispose();
            helper_stream = null;
        }
    }
}
