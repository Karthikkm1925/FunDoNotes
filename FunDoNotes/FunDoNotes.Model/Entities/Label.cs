using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.Model.Entities
{
    public class Label
    {
        public int LabelId { get; set; }
        public string LabelName { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<NoteLabel> NoteLabels { get; set; } = new List<NoteLabel>();


    }
}
