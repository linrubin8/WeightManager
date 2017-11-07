using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FastReport.Export.BIFF8
{
    class BIFF8_ShapeProp
    {
        UInt16 Complex;
        UInt32 Union;
        Byte[] Data;
//        UInt32 Id;

        internal UInt32 ReadProperty(StreamHelper stream)
        {
            Complex = stream.ReadUshort();
            Union = stream.ReadUint();

            if ((Complex & 0x8000) == 0)
            {
                return 6;
            }
            else
            {
                return 6 + Union;
            }
        }

        internal void ReadPayload(StreamHelper stream)
        {
            if ((Complex & 0x8000) != 0)
            {
                Data = stream.ReadBytes( (int) Union);
            }
        }

        internal void WriteProperty(StreamHelper stream)
        {
            stream.WriteUshort(Complex);
            stream.WriteUint(Union);
        }
        internal void WritePayload(StreamHelper stream)
        {
            if ((Complex & 0x8000) != 0)
            {
                stream.WriteBytes(Data);
            }
        }

        public BIFF8_ShapeProp()
        { 
        }
        public BIFF8_ShapeProp(UInt16 Complex, UInt32 Union) 
        { 
            this.Complex = Complex; 
            this.Union = Union; 
        }
        public BIFF8_ShapeProp(UInt16 Complex, UInt32 Union, Byte[] Data) 
        { 
            this.Complex = Complex; 
            this.Union = Union; 
            this.Data = Data; 
        }
        public BIFF8_ShapeProp(UInt16 Complex, string value)
        {
            this.Complex = (ushort) (Complex | 0xc000);
            Data = new byte[value.Length * 2 + 2];
            int i = 0;
            foreach(char ch in value)
            {
                Data[i++] = (byte)ch;
                Data[i++] = (byte)((ushort)ch >> 8);
            }
            Data[i++] = 0;
            Data[i] = 0;
            this.Union = (uint) (this.Data.Length);
        }
    }

    class BIFF8_ExcelDrawing
    {
        // msofbtSp
        UInt32 spid;
        UInt32 grfPersistent;

        // msofbtClientAnchor
        UInt16 unknown;
        UInt16 left_col;
        UInt16 left_off;
        UInt16 top_row;
        UInt16 top_off;
        UInt16 right_col;
        UInt16 right_off;
        UInt16 bottom_row;
        UInt16 bottom_off;

        // ftCmo: Common object data
        UInt16 ObjectType;
        UInt16 ObjectID;
        UInt16 OptionFlags;

        // ftCf: Clipboard format
        UInt16 ftCf;

        // ftPioGrbit: Picture option flags
        UInt16 ftPioGrbit;

        ArrayList prop_list = new ArrayList();

        UInt32 first_shape_id = 1024;

        /// <summary>
        /// Related to DRAWING group. Used once
        /// </summary>
        UInt32 csp;        // The number of shapes in this drawing
        UInt32 spidCur;    // The last MSOSPID given to an SP in this DG

        internal long total_spgr_size_position;
        internal long total_dg_size_position;

        private void ReadSpgrContainer(StreamHelper stream, uint dg_record_size)
        {
            while (dg_record_size > 0)
            {
                UInt16 _packed_data = stream.ReadUshort();
                UInt16 _fbt = stream.ReadUshort();
                UInt32 _total_record_size = stream.ReadUint();

                if (_fbt != 0xf004) throw new Exception("Group container format error");

                ReadSpContainer(stream, _total_record_size);

                dg_record_size -= _total_record_size + 8;
            }
        }

        private void ReadDgContainer(StreamHelper stream, UInt32 container_size)
        {
            while ( container_size > 0 )
            {
                UInt16 dg_packed_data = stream.ReadUshort();
                UInt16 dg_fbt = stream.ReadUshort();
                UInt32 dg_record_size = stream.ReadUint();

                switch (dg_fbt)
                {
                    case 0xf008: // msofbtDg
                        csp = stream.ReadUint();        // The number of shapes in this drawing
                        spidCur = stream.ReadUint();    // The last MSOSPID given to an SP in this DG
                        break;

                    case 0xf003: // msofbtSpgrContainer
                        dg_record_size = container_size - 8;
                        ReadSpgrContainer(stream, dg_record_size);
                        break;

                }

                container_size -= dg_record_size + 8;
            }

        }

        private void ReadSpContainer(StreamHelper stream, UInt32 container_size)
        {
            while (container_size > 0)
            {
                UInt16 sp_packed_data = stream.ReadUshort();
                UInt16 sp_fbt = stream.ReadUshort();
                UInt32 sp_record_size = stream.ReadUint();

                switch (sp_fbt)
                {
                    case 0xf009: // msofbtSpgr
                        UInt32 x = stream.ReadUint();
                        UInt32 y = stream.ReadUint();
                        UInt32 dx = stream.ReadUint();
                        UInt32 dy = stream.ReadUint();
                        break;

                    case 0xf00a: // msofbtSp
                        spid = stream.ReadUint();
                        grfPersistent = stream.ReadUint();
                        break;

                    case 0xf00b: // msofbtOPT
                        UInt32 local_size = sp_record_size;

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

                    case 0xf010: // msofbtClientAnchor
                        unknown = stream.ReadUshort();
                        left_col = stream.ReadUshort();
                        left_off = stream.ReadUshort();
                        top_row = stream.ReadUshort();
                        top_off = stream.ReadUshort();
                        right_col = stream.ReadUshort();
                        right_off = stream.ReadUshort();
                        bottom_row = stream.ReadUshort();
                        bottom_off = stream.ReadUshort();
                        break;

                    case 0xf011: // msofbtClientData
                        stream.SkipBytes((int)sp_record_size);
                        break;

                    default:
                        throw new Exception("Sp container format error");
                }

                container_size -= sp_record_size + 8;
            }

        }

        internal void Read(StreamHelper stream, UInt32 record_size)
        {
            UInt16 packed_data = stream.ReadUshort();
            UInt16 fbt = stream.ReadUshort();  
            UInt32 total_record_size = stream.ReadUint();

            record_size -= 8;

            switch (fbt)
            {
                case 0xf002:    // DgContainer
                    ReadDgContainer(stream, record_size);
                    break;

                case 0xf004:    // SpContainer
                    ReadSpContainer(stream, record_size);
                    break;
                default:
                    throw new Exception("Drawing structure error");

            }
        }

        internal void Read_GraphicsObject(StreamHelper stream, UInt32 RecordSize)
        {
            for (UInt16 sub_size = (UInt16) RecordSize; sub_size > 0; )
            {
                UInt16 SubRecordID = stream.ReadUshort();
                UInt16 SubRecordSize = stream.ReadUshort();

                switch (SubRecordID)
                {
                    case 0x0015: // ftCmo: Common object data
                        ObjectType = stream.ReadUshort();
                        ObjectID = stream.ReadUshort();
                        OptionFlags = stream.ReadUshort();
                        stream.SkipBytes(12);
                        break;

                    case 0x0007: // ftCf: Clipboard format
                        ftCf = stream.ReadUshort();
                        break;

                    case 0x0008: // ftPioGrbit: Picture option flags
                        ftPioGrbit = stream.ReadUshort();
                        break;

                    case 0x0000: // ftEnd: The ftEnd file type marks the end of an OBJ record.
                        if (SubRecordSize != 0)
                        {
                            throw new Exception("Bad forrmat of ftEnd record");
                        }
                        break;

                    default:
                        throw new Exception("Unknown subrecord type in 005dh record");
                }
                sub_size -= (ushort)(SubRecordSize + sizeof(UInt16) + sizeof(UInt16));
            }
        }

        internal void Write_GraphicsObject(StreamHelper stream)
        {
            stream.WriteUshort(0x005d);  // GOBJ
            stream.WriteUshort(38);

            stream.WriteUshort(0x0015);  // GOBJ::ftCmo
            stream.WriteUshort(18);
            stream.WriteUshort(ObjectType);
            stream.WriteUshort(ObjectID);
            stream.WriteUshort(OptionFlags);
            stream.SkipBytes(12);

            stream.WriteUshort(0x0007);  // GOBJ::ftCf
            stream.WriteUshort(2);
            stream.WriteUshort(ftCf);

            stream.WriteUshort(0x0008);  // GOBJ::ftPioGrbit
            stream.WriteUshort(2);
            stream.WriteUshort(ftPioGrbit);

            stream.WriteUshort(0x0000);  // GOBJ::ftEnd
            stream.WriteUshort(0);
        }

        private void WriteSpOptions(StreamHelper stream)
        {
            stream.WriteUshort(0x00b3);
            stream.WriteUshort(0xf00b);
            long opt_size_position = stream.Position;
            stream.WriteUint(0x0000);

            foreach (BIFF8_ShapeProp prop in prop_list) prop.WriteProperty(stream);
            foreach (BIFF8_ShapeProp prop in prop_list) prop.WritePayload(stream);

            // Fix options size
            long curr_position = stream.Position;
            stream.Position = opt_size_position;
            opt_size_position = curr_position - opt_size_position - sizeof(UInt32);
            stream.WriteUint((ushort)opt_size_position);
            stream.Position = curr_position;
        }

        private uint Write_SpContainer(StreamHelper stream)
        {
            stream.WriteUshort(0x000f);  
            stream.WriteUshort(0xf004);  // SpContainer
            long container_size_position = stream.Position;
            stream.WriteUint(0x0000);

            // SP
            stream.WriteUshort(0x04b2);  
            stream.WriteUshort(0xf00a);
            stream.WriteUint(0x0008);
            stream.WriteUint(spid);
            stream.WriteUint(grfPersistent);

            // OPT
            WriteSpOptions(stream);

            // ClientAnchor
            stream.WriteUshort(0x0000);
            stream.WriteUshort(0xf010);
            stream.WriteUint(0x0012);
            stream.WriteUshort(unknown);
            stream.WriteUshort(left_col);
            stream.WriteUshort(left_off);
            stream.WriteUshort(top_row);
            stream.WriteUshort(top_off);
            stream.WriteUshort(right_col);
            stream.WriteUshort(right_off);
            stream.WriteUshort(bottom_row);
            stream.WriteUshort(bottom_off);

            // ClientData
            stream.WriteUshort(0x0000);
            stream.WriteUshort(0xf011);
            stream.WriteUint(0x0000);

            long curr_position = stream.Position;
            stream.Position = container_size_position;
            container_size_position = curr_position - container_size_position - sizeof(UInt32);
            stream.WriteUint((ushort)container_size_position);
            stream.Position = curr_position;

            return (uint) (container_size_position + 8);
        }

        private uint Write_SpgrContainer(StreamHelper stream)
        {
            stream.WriteUshort(0x000f);  // SPGR
            stream.WriteUshort(0xf003);
            total_spgr_size_position = stream.Position;
            stream.WriteUint(0x0000);

            stream.WriteUshort(0x000f); // SpContainer
            stream.WriteUshort(0xf004);  
            stream.WriteUint(0x0028);

            stream.WriteUshort(0x0001); // msofbtSpgr
            stream.WriteUshort(0xf009);
            stream.WriteUint(0x0010);
            stream.WriteUint(0);    // x
            stream.WriteUint(0);    // y
            stream.WriteUint(0);    // dx 
            stream.WriteUint(0);    // dy

            // SP
            stream.WriteUshort(0x0002);
            stream.WriteUshort(0xf00a);
            stream.WriteUint(0x0008);
            stream.WriteUint(first_shape_id /*1024*/);  // First shape ID 1024
            stream.WriteUint(5);     // Bitfield for root record.

            uint container_size = Write_SpContainer(stream);
            return container_size + 48;
        }

        internal uint Write(StreamHelper stream, bool drawing_group)
        {
            uint multiple_sp_size = 0;

            stream.WriteUshort(0x00ec);  // DRAWING
            long drawing_size_position = stream.Position;
            stream.WriteUshort(0x0000);

            if (drawing_group)
            {
                // DG container
                stream.WriteUshort(0x000f);
                stream.WriteUshort(0xf002);
                total_dg_size_position = stream.Position;
                stream.WriteUint(0x0000);

                // DG
                stream.WriteUshort(0x0010);
                stream.WriteUshort(0xf008);     // DG
                stream.WriteUint(0x0008);
                stream.WriteUint(csp+1);        // The number of shapes in this drawing
                stream.WriteUint(spidCur);    // The last MSOSPID given to an SP in this DG

                multiple_sp_size += Write_SpgrContainer(stream);
            }
            else
            {
                multiple_sp_size += Write_SpContainer(stream);
            }

            long curr_position = stream.Position;
            stream.Position = drawing_size_position;
            drawing_size_position = curr_position - drawing_size_position - sizeof(UInt16);
            stream.WriteUshort((ushort)drawing_size_position);
            stream.Position = curr_position;

            return multiple_sp_size;
        }

        internal void AddPicureToSheet(
            FastReport.Export.ExportIEMObject Obj, int index, int x, int y, int dx, int dy)
        {
            this.csp = (uint) (index + 1);
            this.spidCur = this.csp + first_shape_id;

            this.unknown = 2;
            this.left_col = (ushort)x;
            this.left_off = 0;
            this.top_row = (ushort)y;
            this.top_off = 0;
            this.right_col = (ushort)(x + dx);
            this.right_off = 0;
            this.bottom_row = (ushort)(y + dy);
            this.bottom_off = 0;

            this.right_off = 3; // 256;
            this.bottom_off = 3; // 205;

            this.spid = spidCur;
            this.grfPersistent = 0xa00;

            // ftCmo: Common object data
            this.ObjectType = 0x0008;
            this.ObjectID = (ushort) csp; // 0x002a;
            this.OptionFlags = 0x6011;

            this.ftCf = 0xffff;
            this.ftPioGrbit = 0x0001;

            prop_list.Add(new BIFF8_ShapeProp(127, 0x01fb0080)); // fLockAgainstGrouping
            prop_list.Add(new BIFF8_ShapeProp(191, 0x00040004)); // fFitTextToShape
            prop_list.Add(new BIFF8_ShapeProp(260 | 0x4000, csp)); // fFitTextToShape
            prop_list.Add(new BIFF8_ShapeProp(261, "pib" + csp.ToString()+ ".png")); // pibName
            prop_list.Add(new BIFF8_ShapeProp(319, 0x00060000)); // pictureActive
            prop_list.Add(new BIFF8_ShapeProp(447, 0x00110000)); // fNoFillHitTest
            prop_list.Add(new BIFF8_ShapeProp(511, 0x00180010)); // fNoLineDrawDash
            prop_list.Add(new BIFF8_ShapeProp(831, 0x00180010)); // fBackground
            prop_list.Add(new BIFF8_ShapeProp(896, "pic" + csp.ToString())); // wzName
            prop_list.Add(new BIFF8_ShapeProp(897, "")); // wzDescription is empty
            prop_list.Add(new BIFF8_ShapeProp(959, 0x00020000)); // ??
        }

        internal BIFF8_ExcelDrawing()
        {
        }
    }
}
