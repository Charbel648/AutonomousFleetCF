using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddTelemetryAndPriorityFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_TenantId_OrderId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "orders",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "orders",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "PickupAddress",
                table: "orders",
                newName: "pickup_address");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "orders",
                newName: "last_updated_at");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress",
                table: "orders",
                newName: "delivery_address");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "orders",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "orders",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AssignedVehicleId",
                table: "orders",
                newName: "assigned_vehicle_id");

            migrationBuilder.AlterColumn<string>(
                name: "pickup_address",
                table: "orders",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "delivery_address",
                table: "orders",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<DateTime>(
                name: "assigned_at",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "cancelled_at",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "completed_at",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "failed_at",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "priority_score",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "queued_at",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "started_at",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_tenant_id",
                table: "orders",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_tenant_id_status",
                table: "orders",
                columns: new[] { "tenant_id", "status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_tenant_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_tenant_id_status",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "assigned_at",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "cancelled_at",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "completed_at",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "failed_at",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "priority_score",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "queued_at",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "started_at",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                table: "orders",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "pickup_address",
                table: "orders",
                newName: "PickupAddress");

            migrationBuilder.RenameColumn(
                name: "last_updated_at",
                table: "orders",
                newName: "LastUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "delivery_address",
                table: "orders",
                newName: "DeliveryAddress");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "orders",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "orders",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "assigned_vehicle_id",
                table: "orders",
                newName: "AssignedVehicleId");

            migrationBuilder.AlterColumn<string>(
                name: "PickupAddress",
                table: "orders",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryAddress",
                table: "orders",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.CreateIndex(
                name: "IX_orders_TenantId_OrderId",
                table: "orders",
                columns: new[] { "TenantId", "OrderId" });
        }
    }
}
