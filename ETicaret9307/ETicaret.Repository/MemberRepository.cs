using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eticaret.Common;
using ETicaret.Entity;

namespace ETicaret.Repository
{
    public class MemberRepository : DataRepository<Member, int>
    {
        public static ECommerceEntities db = Tool.GetConnection();
        ResultProcess<Member> result = new ResultProcess<Member>();
        public override Result<int> Delete(int id)
        {
            Member m = db.Members.SingleOrDefault(t => t.UserId == id);
            db.Members.Remove(m);
            return result.GetResult(db);
        }

        public override Result<Member> GetObjById(int id)
        {
            Member m = db.Members.SingleOrDefault(t => t.UserId == id);
            return result.GetT(m);

        }

        public override Result<int> Insert(Member item)
        {
            Member newMember = db.Members.Create();

            newMember.FirstName = item.FirstName;
            newMember.LastName = item.LastName;
            newMember.Email = item.Email;
            newMember.Password = item.Password;
            newMember.RoleId = item.RoleId;
            newMember.Address = item.Address;

            db.Members.Add(newMember);
            return result.GetResult(db);

        }

        public override Result<List<Member>> List()
        {
            return result.GetListResult(db.Members.ToList());
        }

        public override Result<int> Update(Member item)
        {
            Member updateMember = GetObjById(item.UserId).ProcessResult;

            updateMember.FirstName = item.FirstName;
            updateMember.LastName = item.LastName;
            updateMember.Address = item.Address;
            updateMember.Password = item.Password;

            return result.GetResult(db);
        }
    }
}
