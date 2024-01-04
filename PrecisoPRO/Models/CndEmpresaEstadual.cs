using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace PrecisoPRO.Models

{
    [Table("CNDEMPRESAS_E")]
    public class CndEmpresaEstadual
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        [Column("ID")]
        public int Id { get; set; }


        [Display(Name = "Empresa")]
        [ForeignKey("Empresa")]
        [Column("ID_EMPRESA")]
        public int IdEmpresa { get; set; }


        //foreignkey
        public Empresa? Empresa { get; set; }


        [Display(Name = "Número da CND")]
        [Column("NO_CND")]
        public string? NCnd { get; set; }

        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        [Column("DATA_CAD")]
        public DateTime? Data_Cad { get; set; }

        [Display(Name = "Cadastro")]
        public string? DataCad
        {
            get { return Data_Cad?.ToShortDateString(); }
        }

        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        [Column("DATA_VENC")]
        public DateTime? Data_Venc { get; set; }


        [Display(Name = "Vendimento")]
        public string? DataVenc
        {
            get { return Data_Venc?.ToShortDateString(); }
        }

        [Display(Name = "Status")]
        [Column("STATUS")]
        public sbyte Status { get; set; }



        [Display(Name = "Estado")]
        [Column("UF")]
        public string? Uf { get; set; }

        [Display(Name = "Arquivo PDF")]
        [Column("ARQUIVO_PDF")]
        public string? PdfCnd { get; set; }

    }
}
