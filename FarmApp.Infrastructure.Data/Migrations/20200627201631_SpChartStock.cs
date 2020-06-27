using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FarmApp.Infrastructure.Data.Migrations
{
    public partial class SpChartStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[ChartStock]
AS
BEGIN
	SELECT 
		r1.RegionName Country
		, r2.RegionName Region
		, ISNULL(r3.RegionName, r2.RegionName) City
		, p2.PharmacyName ParentPharmacy
		, p1.PharmacyName
		, d.DrugName
		, d.IsGeneric
		, s.CreateDate
		, s.Quantity
	FROM dist.Regions r1
		INNER JOIN dist.Regions r2 ON r2.RegionId = r1.Id
		LEFT JOIN dist.Regions r3 ON r3.RegionId = r2.Id
		INNER JOIN dist.Pharmacies p1 ON p1.RegionId = ISNULL(r3.Id, r2.Id)
		INNER JOIN dist.Pharmacies p2 ON p2.Id = ISNULL(p1.PharmacyId, p1.Id)
		INNER JOIN tab.Stocks s ON s.PharmacyId = ISNULL(p1.Id, p2.Id)
		INNER JOIN tab.Drugs d ON d.Id = s.DrugId
			WHERE r1.RegionTypeId = 1
END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
