using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartManut.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialSqlServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fornecedores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_empresa = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    contato = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    prazo_entrega_dias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "perfis",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_cargo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    permissoes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pecas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo_sku = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    quantidade_estoque = table.Column<int>(type: "int", nullable: false),
                    estoque_minimo = table.Column<int>(type: "int", nullable: false),
                    custo_unitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    fornecedor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pecas", x => x.id);
                    table.ForeignKey(
                        name: "FK_pecas_fornecedores_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "utilizadores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    senha_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    perfil_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utilizadores", x => x.id);
                    table.ForeignKey(
                        name: "FK_utilizadores_perfis_perfil_id",
                        column: x => x.perfil_id,
                        principalTable: "perfis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "setores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_setor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    localizacao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    responsavel_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setores", x => x.id);
                    table.ForeignKey(
                        name: "FK_setores_utilizadores_responsavel_id",
                        column: x => x.responsavel_id,
                        principalTable: "utilizadores",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "equipamentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    data_aquisicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    setor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipamentos", x => x.id);
                    table.ForeignKey(
                        name: "FK_equipamentos_setores_setor_id",
                        column: x => x.setor_id,
                        principalTable: "setores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ordens_servico",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    prioridade = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    descricao_problema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_abertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_fechamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    equipamento_id = table.Column<int>(type: "int", nullable: false),
                    solicitante_id = table.Column<int>(type: "int", nullable: false),
                    tecnico_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordens_servico", x => x.id);
                    table.ForeignKey(
                        name: "FK_ordens_servico_equipamentos_equipamento_id",
                        column: x => x.equipamento_id,
                        principalTable: "equipamentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordens_servico_utilizadores_solicitante_id",
                        column: x => x.solicitante_id,
                        principalTable: "utilizadores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordens_servico_utilizadores_tecnico_id",
                        column: x => x.tecnico_id,
                        principalTable: "utilizadores",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "historico_manutencoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ordem_servico_id = table.Column<int>(type: "int", nullable: false),
                    peca_id = table.Column<int>(type: "int", nullable: true),
                    quantidade_aplicada = table.Column<int>(type: "int", nullable: false),
                    relatorio_tecnico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tempo_gasto_horas = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    data_registro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historico_manutencoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_historico_manutencoes_ordens_servico_ordem_servico_id",
                        column: x => x.ordem_servico_id,
                        principalTable: "ordens_servico",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historico_manutencoes_pecas_peca_id",
                        column: x => x.peca_id,
                        principalTable: "pecas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_equipamentos_setor_id",
                table: "equipamentos",
                column: "setor_id");

            migrationBuilder.CreateIndex(
                name: "IX_historico_manutencoes_ordem_servico_id",
                table: "historico_manutencoes",
                column: "ordem_servico_id");

            migrationBuilder.CreateIndex(
                name: "IX_historico_manutencoes_peca_id",
                table: "historico_manutencoes",
                column: "peca_id");

            migrationBuilder.CreateIndex(
                name: "IX_ordens_servico_equipamento_id",
                table: "ordens_servico",
                column: "equipamento_id");

            migrationBuilder.CreateIndex(
                name: "IX_ordens_servico_solicitante_id",
                table: "ordens_servico",
                column: "solicitante_id");

            migrationBuilder.CreateIndex(
                name: "IX_ordens_servico_tecnico_id",
                table: "ordens_servico",
                column: "tecnico_id");

            migrationBuilder.CreateIndex(
                name: "IX_pecas_fornecedor_id",
                table: "pecas",
                column: "fornecedor_id");

            migrationBuilder.CreateIndex(
                name: "IX_setores_responsavel_id",
                table: "setores",
                column: "responsavel_id");

            migrationBuilder.CreateIndex(
                name: "IX_utilizadores_perfil_id",
                table: "utilizadores",
                column: "perfil_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "historico_manutencoes");

            migrationBuilder.DropTable(
                name: "ordens_servico");

            migrationBuilder.DropTable(
                name: "pecas");

            migrationBuilder.DropTable(
                name: "equipamentos");

            migrationBuilder.DropTable(
                name: "fornecedores");

            migrationBuilder.DropTable(
                name: "setores");

            migrationBuilder.DropTable(
                name: "utilizadores");

            migrationBuilder.DropTable(
                name: "perfis");
        }
    }
}
