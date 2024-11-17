using FormManagement.Entities;

namespace FormManagement.Business
{
    public interface IFormService
    {
        List<Form> GetAllForms();
        void CreateForm(Form form);
    }
}
