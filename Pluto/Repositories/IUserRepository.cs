
using Pluto.Models;

using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface IUserRepository
    {
        Reponse ListeUser(string type, string tokenKey);
        Reponse ChercherUser(long? id, string type, string tokenKey);
        Reponse AjouterPersonnal(Personnal personnal, string tokenKey);
        Reponse AjouterStudent(StudentList student, string tokenKey);
        Reponse AjouterTeacher(Teacher teacher, string tokenKey);
        Reponse AjouterParent(Parent parent, string tokenKey);

        Reponse bloquerUser(long? id, string tokenKey);
    }
}
