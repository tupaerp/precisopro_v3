using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace PrecisoPRO.Models
{
    [Table("CNDCLIENTE_F")]
    public class CndClienteFederal
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        [Column("ID")]
        public int Id { get; set; }


        [Display(Name = "Cliente")]
        [ForeignKey("Cliente")]
        [Column("ID_CLIENTE")]
        public int IdCliente { get; set; }


        //foreignkey
        public Cliente? Cliente { get; set; }


     

        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        [Column("DATA_CAD")]
        public DateTime? Data_Emissao { get; set; }

        //Usar esse campo para index
        [Display(Name = "Data da Emissão")]
        public string? DataEmissao
        {
            get { return Data_Emissao?.ToShortDateString(); }
        }


        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        [Column("DATA_VENC")]
        public DateTime? Data_Venc { get; set; }



        //Usar esse campo para index
        [Display(Name = "Vencimento")]
        public string? DataVenc
        {
            get { return Data_Venc?.ToShortDateString(); }
        }

        //Calcular dias restante
        public string DiasRestantes
        {
            get
            {
                int diasRestantes = CalcularDiasRestantes();
                return $"{diasRestantes} dias";
            }
        }

        // Método privado para calcular os dias restantes
        private int CalcularDiasRestantes()
        {
            DateTime dataAtual = DateTime.Now;
            TimeSpan diferenca = (TimeSpan)(Data_Venc - dataAtual);
            return (int)Math.Ceiling(diferenca.TotalDays);
        }


        //O Statuso pode ser:
        /**
         * 1 - PROCESSAMENTO OK - CERTIDÃO ENCONTRADA
         * 2 - PROCESSAMENTO OK - CERTIDÃO EMITIDA
         * 3 - PROCESSAMENTO OK - CERTIDÃO NÃO EMITIDA
         * 4 - PROCESSAMENTO OK - CERTIDAO NAO EMITIDA. SITUÃÇÃO CADASTRAL IMPEDITIVA
         * 5 - PROCESSAMENTO OK - ANALISE INCONSISTENTE. TENTE NOVAMENTE
         * 6 - PROCESSAMENTO OK - BASE DE APOIO A VERIFICACAO INDISPONIVEL - TENTE NOVAMENTE
         * 7 - EM PREOCESSAMENTO: RETORNE NOVAMENTE COM A CHAVE
         * 8 - CONTRIBUINTE NÃO ENCONTRADO
         * 9 - PARAMETROS INVÁLIDOS
         * 10 - TIPO DE CONTRIBUINTE INVÁLIDO
         * 11 - CONTRIBUINTE NAO ENCONTRADO
         * 12 - CODIGO DE IDENTIFICACAO INVALIDO
         * 13 - CHAVE INVÁLIDA
         * 14 - CHAVE NÃO ENCONTRADA
         * 15 - CHAVE PARA IMOVEL RURAL
         * 99 - ERRO NO SERVIDOR
         */

        [Display(Name = "Status")] 
        [Column("STATUS")]
        public sbyte Status { get; set; }

        [Display(Name = "Tipo de Certidao")] //1-NEGATIVA; 2-POSITIVA COM EFEITO DE NEGATIVA
        [Column("TIPO_CERTIDAO")]
        public sbyte TipoCertidao { get; set; }



        [Display(Name = "Código CND")]
        [Column("COD_CONTROLE")]
        public string? CodControle { get; set; }

        [Display(Name = "Arquivo PDF")]
        [Column("DOC_PDF")]
        public string? PdfCnd { get; set; }
    }
}
