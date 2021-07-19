using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace TaskAPI_2_1.DAL.Migrations
{
    [Migration(6)]
    public class AgentMigration:Migration
    {
        public override void Down()
        {
            Delete.Table("agents");
        }

        public override void Up()
        {
            Create.Table("agents")
                 .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("agentId").AsInt32()
                 .WithColumn("AgentUrl").AsString();
                 
        }

    }
}
