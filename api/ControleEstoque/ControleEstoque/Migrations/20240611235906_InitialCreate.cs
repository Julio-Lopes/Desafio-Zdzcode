using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleEstoque.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE IF EXISTS cliente;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS produto;");
            migrationBuilder.Sql("DROP TABLE IF EXISTS SaidaProduto;");

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaidaProduto",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idProduto = table.Column<int>(type: "INTEGER", nullable: false),
                    idCliente = table.Column<int>(type: "INTEGER", nullable: false),
                    DataHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    NomeProduto = table.Column<string>(type: "TEXT", nullable: true),
                    NomeCliente = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaidaProduto", x => x.id);
                });

               migrationBuilder.Sql(
                    @"
                    CREATE TRIGGER atualizar_quantidade_produto
                    AFTER INSERT ON saidaproduto
                    FOR EACH ROW
                    BEGIN
                        UPDATE produto
                        SET quantidade = quantidade - NEW.quantidade
                        WHERE id = NEW.idProduto;
                    END;
                    "
    );

               migrationBuilder.Sql(
                    @"
                    CREATE TRIGGER devolver_quantidade_produto
                    AFTER DELETE ON saidaproduto
                    FOR EACH ROW
                    BEGIN
                        UPDATE produto
                        SET quantidade = quantidade + OLD.quantidade
                        WHERE id = OLD.idProduto;
                    END;
                    "
    );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "produto");

            migrationBuilder.DropTable(
                name: "SaidaProduto");
        }
    }
}
