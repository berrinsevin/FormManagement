using FormManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FormManagement.DataAccess
{

    public class FormRepository : IFormRepository
    {
        private readonly FormDbContext _context;
        public FormRepository(FormDbContext context)
        {
            _context = context;
        }
        public List<Form> GetAllForms()
        {
            return _context.Forms.Include(f => f.Fields).ToList();
        }
        public void CreateForm(Form form)
        {
            _context.Forms.Add(form);
            foreach (var field in form.Fields)
            {
                _context.Fields.Add(field);
            }
            _context.SaveChanges();
        }
    }
}