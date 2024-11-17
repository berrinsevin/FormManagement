using FormManagement.Entities;

namespace FormManagement.DataAccess
{
    public interface IFormRepository
    {
        List<Form> GetAllForms();
        void CreateForm(Form form);
    }
}
