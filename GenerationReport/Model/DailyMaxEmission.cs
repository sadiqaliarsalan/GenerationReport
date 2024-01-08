using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerationReport.Model
{
    public class DailyMaxEmission
    {
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public double Emission { get; set; }
    }
}
