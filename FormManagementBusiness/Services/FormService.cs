using FormManagement.Business;
using FormManagement.DataAccess;
using FormManagement.Entities;

namespace FormManagement.Business
{
    public class FormService : IFormService
    {
        private readonly IFormRepository _formRepository;

        public FormService(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public List<Form> GetAllForms()
        {
            return _formRepository.GetAllForms();
        }

        public void CreateForm(Form form)
        {
            form.CreatedAt = DateTime.Now;
            _formRepository.CreateForm(form);
        }
    }
}
