using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace TaskAPI_2_1.DAL.Migrations
{
    [Migration(4)]
    public class NetWorkAgentMigration:Migration
    {
        public override void Down()
        {
            Delete.Table("networkagentmetrics");
        }

        public override void Up()
        {
            Create.Table("networkagentmetrics")
                 .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("agentId").AsInt32()
                 .WithColumn("Value").AsInt32()
                 .WithColumn("Time").AsInt64();
        }
    }
}
