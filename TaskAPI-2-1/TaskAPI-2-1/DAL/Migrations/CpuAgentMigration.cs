using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;


namespace TaskAPI_2_1.DAL.Migrations
{
    [Migration(1)]
    public class CpuAgentMigration : Migration
    {
        public override void Down()
        {
            Delete.Table("cpuagentmetrics");
         }

        public override void Up()
        {
            Create.Table("cpuagentmetrics")
                 .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("agentId").AsInt32()
                 .WithColumn("Value").AsInt32()
                 .WithColumn("Time").AsInt64();
        }
    }
}
