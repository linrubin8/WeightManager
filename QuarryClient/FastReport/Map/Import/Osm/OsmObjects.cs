using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Map.Import.Osm
{
    internal class OsmNode
    {
        #region Fields

        private double lat;
        private double lon;
        private bool hasParent;
        private Dictionary<string, string> tags;

        #endregion // Fields

        #region Properties

        public double Lat
        {
            get { return lat; }
            set { lat = value; }
        }

        public double Lon
        {
            get { return lon; }
            set { lon = value; }
        }

        public bool HasParent
        {
            get { return hasParent; }
            set { hasParent = value; }
        }

        public Dictionary<string, string> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        #endregion // Properties

        #region Constructors

        public OsmNode(double lat, double lon)
        {
            this.lat = lat;
            this.lon = lon;
            hasParent = false;
        }

        public OsmNode(double lat, double lon, Dictionary<string, string> tags)
        {
            this.lat = lat;
            this.lon = lon;
            hasParent = false;
            this.tags = tags;
        }

        #endregion // Constructors
    }

    internal class OsmWay
    {
        #region Fields

        private List<ulong> nodeRefs;
        private Dictionary<string, string> tags;

        #endregion // Fields

        #region Properties

        public List<ulong> NodeRefs
        {
            get { return nodeRefs; }
            set { nodeRefs = value; }
        }

        public Dictionary<string, string> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        #endregion // Properties

        #region Constructors

        public OsmWay(List<ulong> nodeRefs)
        {
            this.nodeRefs = nodeRefs;
        }

        public OsmWay(List<ulong> nodeRefs, Dictionary<string, string> tags)
        {
            this.nodeRefs = nodeRefs;
            this.tags = tags;
        }

        #endregion // Constructors
    }

    internal class OsmRelation
    {
        #region Fields

        private List<ulong> nodeRefs;
        private List<ulong> wayRefs;

        #endregion // Fields

        #region Properties

        public List<ulong> NodeRefs
        {
            get { return nodeRefs; }
            set { nodeRefs = value; }
        }

        public List<ulong> WayRefs
        {
            get { return wayRefs; }
            set { wayRefs = value; }
        }

        #endregion // Properties

        #region Constructors

        public OsmRelation(List<ulong> nodeRefs, List<ulong> wayRefs)
        {
            this.nodeRefs = nodeRefs;
            this.wayRefs = wayRefs;
        }

        #endregion // Constructors
    }
}
