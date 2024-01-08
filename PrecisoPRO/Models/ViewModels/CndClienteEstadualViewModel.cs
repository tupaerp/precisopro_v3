using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrecisoPRO.Models.ViewModels
{
    public interface CndClienteEstadualViewModel
    {
        public int Id { get; set; }


       
        public int IdEmpresa { get; set; }


       
        public string? NCnd { get; set; }

        public DateTime? Data_Cad { get; set; }

       
        public string? DataCad
        {
            get { return Data_Cad?.ToShortDateString(); }
        }

        
        public DateTime? Data_Venc { get; set; }


        
        public string? DataVend
        {
            get { return Data_Venc?.ToShortDateString(); }
        }

        
        public sbyte Status { get; set; }



      
        public string? Uf { get; set; }

       
        public string? PdfCnd { get; set; }
    }
}
