using LocalDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestLocalDB
{
    [TestClass]
    public class UnitTestDatabaseManager
    {
        [TestMethod]
        public void TestProduct()
        {
            Table table1 = new Table
            {
                Columns = new List<Column>
                {
                    new Column
                    {
                        Name = "ID",
                        Type = "INT"
                    }
                },
                Rows = new List<Row>
                {
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "ID", "1" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "ID", "2" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "ID", "3" }
                        }
                    }
                }
            };

            Table table2 = new Table
            {
                Columns = new List<Column>
                {
                    new Column
                    {
                        Name = "Name",
                        Type = "STRING"
                    }
                },
                Rows = new List<Row>
                {
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "Name", "abc" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "Name", "def" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "Name", "ghi" }
                        }
                    }
                }
            };

            Table table3 = new Table
            {
                Columns = new List<Column>
                {
                    new Column
                    {
                        Name = "Group",
                        Type = "CHAR"
                    }
                },
                Rows = new List<Row>
                {
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "Group", "A" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "Group", "B" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "Group", "C" }
                        }
                    }
                }
            };

            Table table12 = new Table
            {
                Columns = new List<Column>
                {
                    new Column
                    {
                        Name = "A.ID",
                        Type = "INT"
                    },
                    new Column
                    {
                        Name = "B.Name",
                        Type = "STRING"
                    }
                },
                Rows = new List<Row>
                {
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "1" },
                            { "B.Name", "abc" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "1" },
                            { "B.Name", "def" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "1" },
                            { "B.Name", "ghi" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "2" },
                            { "B.Name", "abc" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "2" },
                            { "B.Name", "def" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "2" },
                            { "B.Name", "ghi" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "3" },
                            { "B.Name", "abc" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "3" },
                            { "B.Name", "def" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "3" },
                            { "B.Name", "ghi" }
                        }
                    }
                }
            };
            Table result1 = DatabaseManager.CartesianProduct(table1, table2);

            Table table23 = new Table
            {
                Columns = new List<Column>
                {
                    new Column
                    {
                        Name = "A.Name",
                        Type = "STRING"
                    },
                    new Column
                    {
                        Name = "B.Group",
                        Type = "CHAR"
                    }
                },
                Rows = new List<Row>
                {
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.Name", "abc" },
                            { "B.Group", "A" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.Name", "abc" },
                            { "B.Group", "B" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.Name", "abc" },
                            { "B.Group", "C" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.Name", "def" },
                            { "B.Group", "A" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.Name", "def" },
                            { "B.Group", "B" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.Name", "def" },
                            { "B.Group", "C" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.Name", "ghi" },
                            { "B.Group", "A" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.Name", "ghi" },
                            { "B.Group", "B" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.Name", "ghi" },
                            { "B.Group", "C" }
                        }
                    }
                }
            };
            Table result2 = DatabaseManager.CartesianProduct(table2, table3);

            Table table13 = new Table
            {
                Columns = new List<Column>
                {
                    new Column
                    {
                        Name = "A.ID",
                        Type = "INT"
                    },
                    new Column
                    {
                        Name = "B.Group",
                        Type = "CHAR"
                    }
                },
                Rows = new List<Row>
                {
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "1" },
                            { "B.Group", "A" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "1" },
                            { "B.Group", "B" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "1" },
                            { "B.Group", "C" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "2" },
                            { "B.Group", "A" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "2" },
                            { "B.Group", "B" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "2" },
                            { "B.Group", "C" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "3" },
                            { "B.Group", "A" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "3" },
                            { "B.Group", "B" }
                        }
                    },
                    new Row
                    {
                        Values = new Dictionary<string, string>
                        {
                            { "A.ID", "3" },
                            { "B.Group", "C" }
                        }
                    }
                }
            };
            Table result3 = DatabaseManager.CartesianProduct(table1, table3);

            Assert.AreEqual(true, this.CompareTables(result1, table12));
            Assert.AreEqual(true, this.CompareTables(result2, table23));
            Assert.AreEqual(true, this.CompareTables(result3, table13));
        }

        private bool CompareTables(Table table1, Table table2)
        {
            if ((table1 == null) != (table2 == null))
                return false;

            if (table1 == null)
                return true;

            if (table1.Rows.Count != table2.Rows.Count || table1.Columns.Count != table2.Columns.Count)
                return false;

            for (int i = 0; i < table1.Columns.Count; ++i)
                if (!string.Equals(table1.Columns[i].Name, table2.Columns[i].Name) || !string.Equals(table1.Columns[i].Type, table2.Columns[i].Type))
                    return false;

            for (int i = 0; i < table1.Rows.Count; ++i)
            {
                foreach (var v in table1.Rows[i].Values)
                    if (!string.Equals(v.Value, table2.Rows[i].Values[v.Key]))
                        return false;

                foreach (var v in table2.Rows[i].Values)
                    if (!string.Equals(v.Value, table1.Rows[i].Values[v.Key]))
                        return false;
            }

            return true;
        }
    }
}
