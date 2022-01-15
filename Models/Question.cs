using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgresConnect.Models
{
    public class Question
    {
        public int id { get; set; }
        public int board_id { get; set; }
        public string title { get; set; }
        public int user_id { get; set; }
        public string description { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? update_date { get; set; }
        public string? answer { get; set; }
        public int? answer_user_id { get; set; }
        public DateTime? answer_create_date { get; set; }
        public DateTime? answer_update_date { get; set; }

    }
}
