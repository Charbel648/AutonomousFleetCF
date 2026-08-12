using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddTelemetryAndPriorityFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_vehicles_TenantId_VehicleId",
                table: "vehicles");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "vehicles",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "vehicles",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "vehicles",
                newName: "latitude");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "vehicles",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "vehicles",
                newName: "registration_number");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "vehicles",
                newName: "last_updated_at");

            migrationBuilder.RenameColumn(
                name: "LastTelemetryAt",
                table: "vehicles",
                newName: "last_telemetry_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "vehicles",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BatteryLevel",
                table: "vehicles",
                newName: "battery_level");

            migrationBuilder.RenameIndex(
                name: "IX_vehicles_TenantId_RegistrationNumber",
                table: "vehicles",
                newName: "IX_vehicles_tenant_id_registration_number");

            migrationBuilder.AlterColumn<decimal>(
                name: "longitude",
                table: "vehicles",
                type: "numeric(9,6)",
                precision: 9,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,7)",
                oldPrecision: 10,
                oldScale: 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "latitude",
                table: "vehicles",
                type: "numeric(9,6)",
                precision: 9,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,7)",
                oldPrecision: 10,
                oldScale: 7);

            migrationBuilder.AlterColumn<string>(
                name: "registration_number",
                table: "vehicles",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "battery_level",
                table: "vehicles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "telemetry_data",
                table: "vehicles",
                type: "jsonb",
                nullable: false,
                defaultValueSql: "'{}'::jsonb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "telemetry_data",
                table: "vehicles");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "vehicles",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "vehicles",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "vehicles",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                table: "vehicles",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "registration_number",
                table: "vehicles",
                newName: "RegistrationNumber");

            migrationBuilder.RenameColumn(
                name: "last_updated_at",
                table: "vehicles",
                newName: "LastUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "last_telemetry_at",
                table: "vehicles",
                newName: "LastTelemetryAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "vehicles",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "battery_level",
                table: "vehicles",
                newName: "BatteryLevel");

            migrationBuilder.RenameIndex(
                name: "IX_vehicles_tenant_id_registration_number",
                table: "vehicles",
                newName: "IX_vehicles_TenantId_RegistrationNumber");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "vehicles",
                type: "numeric(10,7)",
                precision: 10,
                scale: 7,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(9,6)",
                oldPrecision: 9,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "vehicles",
                type: "numeric(10,7)",
                precision: 10,
                scale: 7,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(9,6)",
                oldPrecision: 9,
                oldScale: 6);

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "vehicles",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "BatteryLevel",
                table: "vehicles",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_TenantId_VehicleId",
                table: "vehicles",
                columns: new[] { "TenantId", "VehicleId" });
        }
    }
}
