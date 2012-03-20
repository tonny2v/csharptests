using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpMap;
using Npgsql;
using SharpMap.Forms;

namespace SharpmapTest
{
    public partial class TestCustomGeometry : Form
    {
        public TestCustomGeometry()
        {
            InitializeComponent();
        }

        #region Defined functions

        private SharpMap.Map GetCurrentMap(string sqlconn, string[] tableName)
        {
            SharpMap.Map myMap = new SharpMap.Map();

            using (NpgsqlConnection conn = new NpgsqlConnection(sqlconn))
            {
                conn.Open();
                myMap.Size = new System.Drawing.Size(this.Map_mapImage.Size.Width, this.Map_mapImage.Height);
                //myMap.MinimumZoom = 10;
                if (tableName.Contains("vertices_tmp"))
                {
                    //Add node layer
                    SharpMap.Layers.LabelLayer verticesAsLabel = new SharpMap.Layers.LabelLayer("nodes");
                    SharpMap.Layers.VectorLayer vertices_tmp = new SharpMap.Layers.VectorLayer("vertices_tmp");
                    vertices_tmp.DataSource = new SharpMap.Data.Providers.Postgis(conn.ConnectionString, "vertices_tmp", "the_geom");

                    vertices_tmp.Style.Symbol = SharpMap.Styles.VectorStyle.DefaultSymbol;
                    //vertices_tmp.Style.Symbol = Image.FromFile("..\\..\\Node.bmp");
                    vertices_tmp.Style.SymbolScale = (float)0.32;
                    //不消除锯齿
                    vertices_tmp.SmoothingMode = SmoothingMode.None;
                    myMap.Layers.Add(vertices_tmp);

                    //设置节点Label层样式
                    verticesAsLabel.DataSource = vertices_tmp.DataSource;
                    verticesAsLabel.LabelColumn = "id";
                    verticesAsLabel.Style.Font = new Font("Arial", 6, FontStyle.Bold);
                    verticesAsLabel.Style = new SharpMap.Styles.LabelStyle();
                    verticesAsLabel.Style.ForeColor = Color.Red;
                    verticesAsLabel.Style.Offset = new PointF(10, 0);
                    verticesAsLabel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    verticesAsLabel.SmoothingMode = SmoothingMode.AntiAlias;
                    //layASLabel.SRID = 3945; //This is your spatial ref no
                    verticesAsLabel.LabelFilter = SharpMap.Rendering.LabelCollisionDetection.ThoroughCollisionDetection;
                    verticesAsLabel.Style.CollisionDetection = true;
                }

                if (tableName.Contains("road"))
                {
                    roadlayASLabel = new SharpMap.Layers.LabelLayer("road");
                    //设置road图层的数据源和样式
                    SharpMap.Layers.VectorLayer road = new SharpMap.Layers.VectorLayer("road");
                    road.DataSource = new SharpMap.Data.Providers.PostGIS(conn.ConnectionString, "road", "the_geom");
                    //conn.Open();
                    //var indexes = road.DataSource.GetObjectIDsInView(road.DataSource.GetExtents());
                    road.Style.Line = new System.Drawing.Pen(System.Drawing.Color.LightBlue, 1);
                    //不消除锯齿
                    road.SmoothingMode = SmoothingMode.None;
                    myMap.Layers.Add(road);

                    //设置路段Label层样式
                    roadlayASLabel.DataSource = road.DataSource;
                    roadlayASLabel.LabelColumn = "gid";
                    roadlayASLabel.Style.Font = new Font("Arial", 6, FontStyle.Bold);
                    roadlayASLabel.Style = new SharpMap.Styles.LabelStyle();
                    roadlayASLabel.Style.ForeColor = Color.Black;
                    roadlayASLabel.Style.Offset = new PointF(10, 0);
                    roadlayASLabel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    roadlayASLabel.SmoothingMode = SmoothingMode.AntiAlias;
                    //layASLabel.SRID = 3945; //This is your spatial ref no
                    roadlayASLabel.LabelFilter = SharpMap.Rendering.LabelCollisionDetection.ThoroughCollisionDetection;
                    roadlayASLabel.Style.CollisionDetection = true;
                }
                if (tableName.Contains("hyperpath_lyr"))
                {
                    //设置hyperpath_lyr图层的数据源和样式
                    SharpMap.Layers.VectorLayer hyperpath = new SharpMap.Layers.VectorLayer("hyperpath_lyr");
                    hyperpath.DataSource = new SharpMap.Data.Providers.PostGIS(conn.ConnectionString, "hyperpath_lyr", "the_geom");
                    hyperpath.Style.Line = new System.Drawing.Pen(System.Drawing.Color.GreenYellow, 5);
                    myMap.Layers.Add(hyperpath);

                    //设置主题
                    SharpMap.Styles.VectorStyle firstStyle = new SharpMap.Styles.VectorStyle();
                    SharpMap.Styles.VectorStyle lastStyle = new SharpMap.Styles.VectorStyle();
                    firstStyle.Line = new System.Drawing.Pen(System.Drawing.Color.Green, 1);
                    lastStyle.Line = new System.Drawing.Pen(System.Drawing.Color.Blue, 5);
                    //firstStyle.Outline = new Pen(Color.Black);
                    //firstStyle.EnableOutline = true;
                    //lastStyle.Outline = new Pen(Color.Black);
                    //lastStyle.EnableOutline = true;
                    SharpMap.Rendering.Thematics.GradientTheme Theme = new SharpMap.Rendering.Thematics.GradientTheme("choice_possibility", 0, 1, firstStyle, lastStyle);
                    //Theme.FillColorBlend = SharpMap.Rendering.Thematics.ColorBlend.ThreeColors(Color.Yellow, Color.SkyBlue, Color.HotPink);
                    hyperpath.Theme = Theme;
                }

                if (tableName.Contains("popath_lyr"))
                {
                    //设置road图层的数据源和样式
                    SharpMap.Layers.VectorLayer popath_lyr = new SharpMap.Layers.VectorLayer("popath_lyr");
                    popath_lyr.DataSource = new SharpMap.Data.Providers.PostGIS(conn.ConnectionString, "popath_lyr", "the_geom");
                    //conn.Open();
                    //var indexes = road.DataSource.GetObjectIDsInView(road.DataSource.GetExtents());
                    popath_lyr.Style.Line = new System.Drawing.Pen(System.Drawing.Color.LightBlue, 1);
                    //不消除锯齿
                    popath_lyr.SmoothingMode = SmoothingMode.None;
                    SharpMap.Styles.VectorStyle mystyle = new SharpMap.Styles.VectorStyle();
                    mystyle.Line.Color = Color.Blue;
                    mystyle.Line.Width = 2.0f;
                    popath_lyr.Style = mystyle;
                    myMap.Layers.Add(popath_lyr);



                }

                if (tableName.Contains("regretpath_lyr"))
                {
                    //设置road图层的数据源和样式
                    SharpMap.Layers.VectorLayer regretpath_lyr = new SharpMap.Layers.VectorLayer("regretpath_lyr");
                    regretpath_lyr.DataSource = new SharpMap.Data.Providers.PostGIS(conn.ConnectionString, "regretpath_lyr", "the_geom");
                    //conn.Open();
                    //var indexes = road.DataSource.GetObjectIDsInView(road.DataSource.GetExtents());
                    regretpath_lyr.Style.Line = new System.Drawing.Pen(System.Drawing.Color.LightBlue, 1);
                    //不消除锯齿
                    regretpath_lyr.SmoothingMode = SmoothingMode.None;
                    SharpMap.Styles.VectorStyle mystyle = new SharpMap.Styles.VectorStyle();
                    mystyle.Line.Color = Color.Red;
                    mystyle.Line.Width = 3.5f;
                    regretpath_lyr.Style = mystyle;
                    myMap.Layers.Add(regretpath_lyr);
                }

            }
            return myMap;
        }



        #endregion


        private void TestCustomGeometry_Load(object sender, EventArgs e)
        {
        }

        private void RefreshMap_Click(object sender, EventArgs e)
        {

        }



    }
}