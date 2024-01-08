using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrecisoPRO.Models.ViewDb
{
    //Classe que representa a View do Banco de dados
    [Table("CLIENTE_GERAL")]
    public class ClienteViewGeral
    {
        [Key]
        [Column("ID")]
        public int? IdCliente { get; set; }

        [Column("IE")]
        public string? Ie { get; set; }

        [Column("IM")]
        public string? Im { get; set; }

        [Column("CNPJ")]
        public string? Cnpj { get; set; }

        [Column("RAZAO")]
        public string? RazaoSocial { get; set; }

        [Column("N_MEI")]
        public string? NMei { get; set; }

        [Column("FANTASIA")]
        public string? Fantasia { get; set; }

        [Column("NAT_JURIDICA")]
        public int? NatJuridica { get; set; }

        [Column("ATV_PRINCIPAL")]
        public string? AtvPrincipal { get; set; }

        [Column("CEP")]
        public string? Cep { get; set; }

        [Column("ENDERECO")]
        public string? Endereco { get; set; }

        [Column("NUMERO")]
        public string? Numero { get; set; }

        [Column("COMPLEMENTO")]
        public string? Complemento { get; set; }

        [Column("BAIRRO")]
        public string? Bairro { get; set; }

        [Column("CIDADE")]
        public string? Cidade { get; set; }

        [Column("ESTADO")]
        public string? Estado { get; set; }

        [Column("REFERENCIA")]
        public string? Referencia { get; set; }

        [Column("TELEFONE")]
        public string? Telefone { get; set; }

        [Column("CELULAR")]
        public string? Celular { get; set; }

        [Column("EMAIL")]
        public string? Email { get; set; }

        [Column("DATA_CAD")]
        public string? Data_Cad { get; set; }


        [Column("DATA_ALT")]
        public string? Data_Alt { get; set; }

        [Column("SIT_CADASTRAL")]
        public string? SitCadastral { get; set; }

        [Column("MOT_CREDENCIA_DESCRE")]
        public string? MotDescCred { get; set; }

        [Column("N_NIRE")]
        public string? NNire { get; set; }

        [Column("NOME_CONTADOR")]
        public string? NomeContador { get; set; }

        [Column("CRC_CONTADOR")]
        public string? CrcContador { get; set; }

        [Column("NOME_RESPONSAVEL")]
        public string? NomeResponsavel { get; set; }

        [Column("CRC_RESPONSAVEL")]
        public string? CrcResponsavel { get; set; }


        [Column("TEL_ALTERNATIVO")]
        public string? TelAlterantivo { get; set; }

        [Column("CEL_ALTERNATIVO")]
        public string? CelularAlternativo { get; set; }

        [Column("EMAIL_ALTERNATIVO")]
        public string? EmailAlternativo { get; set; }

        [Column("LOGIN_DTE_SEFAZ")]
        public string? LoginDteSefaz { get; set; }

        [Column("SENHA_DTE_SEFAZ")]
        public string? SenhaDteSefaz { get; set; }


        [Column("CPF_REPRESENTANTE")]
        public string? CpfRepresentante { get; set; }


        [Column("ANOTACOES")]
        public string? Anotacoes { get; set; }

        [Column("REG_JURIDICO")]
        public int? RegJuridico { get; set; }

        [Column("DATA_ABERTURA")]
        public string? DtAbertura { get; set; }

        [Column("ID_NTJ")]
        public int? IdNatJur { get; set; }


        [Column("CODIGO_NTJ")]
        public string? CodigoNtj { get; set; }

        [Column("NOME_NTJ")]
        public string? NomeNtj { get; set; }


        [Column("EMPRESA_ID")]
        public string? EmpresaId { get; set; }

        [Column("EMPRESA_CNPJ")]
        public string? EmpresaCnpj { get; set; }

        [Column("EMPRESA_RAZAO")]
        public string? EmpresaRazao { get; set; }

        [Column("EMPRESA_FANTASIA")]
        public string? EmpresaFantasia { get; set; }




    }
}
