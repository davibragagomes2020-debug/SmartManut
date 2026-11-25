using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartManut.API.Models
{
    [Table("perfis")]
    public class Perfil
    {
        [Key][Column("id")] public int Id { get; set; }
        [Required][Column("nome_cargo")][MaxLength(50)] public string NomeCargo { get; set; } = string.Empty;
        [Column("permissoes")] public string? Permissoes { get; set; }
    }

    [Table("utilizadores")]
    public class Utilizador
    {
        [Key][Column("id")] public int Id { get; set; }
        [Required][Column("nome")][MaxLength(100)] public string Nome { get; set; } = string.Empty;
        [Required][Column("email")][MaxLength(100)] public string Email { get; set; } = string.Empty;
        [Required][Column("senha_hash")][MaxLength(255)] public string SenhaHash { get; set; } = string.Empty;
        [Column("ativo")] public bool Ativo { get; set; } = true;
        [Column("perfil_id")] public int PerfilId { get; set; }
        public Perfil? Perfil { get; set; }
    }

    [Table("setores")]
    public class Setor
    {
        [Key][Column("id")] public int Id { get; set; }
        [Required][Column("nome_setor")][MaxLength(100)] public string NomeSetor { get; set; } = string.Empty;
        [Required][Column("localizacao")][MaxLength(150)] public string Localizacao { get; set; } = string.Empty;
        [Column("responsavel_id")] public int? ResponsavelId { get; set; }
        public Utilizador? Responsavel { get; set; }
    }

    [Table("equipamentos")]
    public class Equipamento
    {
        [Key][Column("id")] public int Id { get; set; }
        [Required][Column("codigo")][MaxLength(50)] public string Codigo { get; set; } = string.Empty;
        [Required][Column("nome")][MaxLength(100)] public string Nome { get; set; } = string.Empty;
        [Required][Column("status")][MaxLength(30)] public string Status { get; set; } = "Ativo";
        [Column("data_aquisicao")] public DateTime DataAquisicao { get; set; } = DateTime.Now;
        [Column("setor_id")] public int SetorId { get; set; }
        public Setor? Setor { get; set; }
    }

    [Table("fornecedores")]
    public class Fornecedor
    {
        [Key][Column("id")] public int Id { get; set; }
        [Required][Column("nome_empresa")][MaxLength(120)] public string NomeEmpresa { get; set; } = string.Empty;
        [Required][Column("contato")][MaxLength(100)] public string Contato { get; set; } = string.Empty;
        [Column("prazo_entrega_dias")] public int PrazoEntregaDias { get; set; } = 7;
    }

    [Table("pecas")]
    public class Peca
    {
        [Key][Column("id")] public int Id { get; set; }
        [Required][Column("codigo_sku")][MaxLength(50)] public string CodigoSku { get; set; } = string.Empty;
        [Required][Column("nome")][MaxLength(100)] public string Nome { get; set; } = string.Empty;
        [Column("quantidade_estoque")] public int QuantidadeEstoque { get; set; }
        [Column("estoque_minimo")] public int EstoqueMinimo { get; set; } = 5;
        [Column("custo_unitario", TypeName = "decimal(10,2)")] public decimal CustoUnitario { get; set; }
        [Column("fornecedor_id")] public int FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
    }

    [Table("ordens_servico")]
    public class OrdemServico
    {
        [Key][Column("id")] public int Id { get; set; }
        [Required][Column("tipo")][MaxLength(20)] public string Tipo { get; set; } = "Corretiva";
        [Required][Column("prioridade")][MaxLength(20)] public string Prioridade { get; set; } = "Média";
        [Required][Column("status")][MaxLength(30)] public string Status { get; set; } = "Aberta";
        [Required][Column("descricao_problema")] public string DescricaoProblema { get; set; } = string.Empty;
        [Column("data_abertura")] public DateTime DataAbertura { get; set; } = DateTime.Now;
        [Column("data_fechamento")] public DateTime? DataFechamento { get; set; }
        [Column("equipamento_id")] public int EquipamentoId { get; set; }
        public Equipamento? Equipamento { get; set; }
        [Column("solicitante_id")] public int SolicitanteId { get; set; }
        public Utilizador? Solicitante { get; set; }
        [Column("tecnico_id")] public int? TecnicoId { get; set; }
        public Utilizador? Tecnico { get; set; }
    }

    [Table("historico_manutencoes")]
    public class HistoricoManutencao
    {
        [Key][Column("id")] public int Id { get; set; }
        [Column("ordem_servico_id")] public int OrdemServicoId { get; set; }
        public OrdemServico? OrdemServico { get; set; }
        [Column("peca_id")] public int? PecaId { get; set; }
        public Peca? Peca { get; set; }
        [Column("quantidade_aplicada")] public int QuantidadeAplicada { get; set; }
        [Required][Column("relatorio_tecnico")] public string RelatorioTecnico { get; set; } = string.Empty;
        [Column("tempo_gasto_horas", TypeName = "decimal(5,2)")] public decimal TempoGastoHoras { get; set; }
        [Column("data_registro")] public DateTime DataRegistro { get; set; } = DateTime.Now;
    }
}
