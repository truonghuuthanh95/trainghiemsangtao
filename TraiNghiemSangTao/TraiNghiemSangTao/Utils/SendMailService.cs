using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using TraiNghiemSangTao.Models.DAO;

namespace TraiNghiemSangTao.Utils
{
    public class SendMailService
    {
        
        public void SendMailToTeacherAsyncRegistrationCreative(RegistrationCreativeExp registrationCreativeExp)
        {
            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("hotanminh@hcm.edu.vn");
            mail.To.Add(registrationCreativeExp.Email.ToString().Trim());
            mail.CC.Add("hotanminh@hcm.edu.vn");
            mail.Subject = "Hoạt động ngoài giờ - Trải nghiệm sáng tạo";
            mail.IsBodyHtml = true;
            string htmlBody = "<h3>Quý thầy cô vừa đăng ký trải nghiệm sáng tạo thành công.</h3>" +
                "<p>- Mã đăng kí: <b>" + registrationCreativeExp.CodeRegisted + "</b></p>" +
                "<p>- Nội dung đăng kí: " + registrationCreativeExp.Program.Name + "</p>" +
                "<p>- Tên trường: " + registrationCreativeExp.School.Name + "</p>" +
                "<p>- Lớp: " + registrationCreativeExp.Class.Name + "</p>" +
                "<p>- Số lượng học sinh: "+ registrationCreativeExp.StudentQuantity + "</p>" +
                "<p>- Buổi tham gia: " + registrationCreativeExp.SessionADay.Name +"</p>" +
                "<p>- Ngày tham gia: " + registrationCreativeExp.DateRegisted.Value.Day.ToString("d2")+"/"+ registrationCreativeExp.DateRegisted.Value.Month.ToString("d2") +"/" + registrationCreativeExp.DateRegisted.Value.Year + "</p>" +
                "<p>- Tên chủ đề đăng kí: " + registrationCreativeExp.ActivitiTitle + "</p>" +
                "<p>- Tên người phụ trách: " + registrationCreativeExp.Creator + "</p>" +
                "<p>- Số điện thoại liên hệ: " + registrationCreativeExp.PhoneNumber + "</p>" +
                "<p>Mọi thắc mắc vui lòng <a href='http://trainghiemthuctien.hcm.edu.vn/lienhe'><i>Liên hệ</i></a></p>";
            mail.Body = htmlBody;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("hotanminh@hcm.edu.vn".Trim(), "Minh1905".Trim());
            smtpClient.EnableSsl = true;
            //smtpClient.Send(mail);
            try
            {
                Thread email = new Thread(delegate ()
                {
                    smtpClient.Send(mail);             
                });
                email.IsBackground = true;
                email.Start();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

        }
        public void SendMailRegistration(Registration registration)
        {
            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("hotanminh@hcm.edu.vn");
            mail.To.Add(registration.Email.ToString().Trim());
            mail.CC.Add("hotanminh@hcm.edu.vn");
            mail.Subject = "Hoạt động ngoài giờ - Kĩ năng khác";
            mail.IsBodyHtml = true;
            string htmlBody = "<h3>Quý thầy cô vừa đăng ký nội dung khác thành công.</h3>" +
                "<p>- Mã đăng kí: <b>" + registration.CodeRegisted + "</b></p>" +
                "<p>- Nội dung thực hiện hoạt động: " + registration.ProgramName + "</p>" +
                "<p>- Tên trường: " + registration.School.Name + "</p>" +
                "<p>- Lớp: " + registration.Class.Name + "</p>" +
                "<p>- Số lượng học sinh: " + registration.StudentQuantity + "</p>" +             
                "<p>- Ngày thực hiện: " + registration.DateRegisted.Value.Day.ToString("d2") + "/" + registration.DateRegisted.Value.Month.ToString("d2") + "/" + registration.DateRegisted.Value.Year + "</p>" +                
                "<p>- Tên người phụ trách: " + registration.Creator + "</p>" +
                "<p>- Số điện thoại liên hệ: " + registration.PhoneNumber + "</p>" +
                "<p>Mọi thắc mắc vui lòng <a href='http://trainghiemthuctien.hcm.edu.vn/lienhe'><i>Liên hệ</i></a></p>";
            mail.Body = htmlBody;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("hotanminh@hcm.edu.vn".Trim(), "Minh1905".Trim());
            smtpClient.EnableSsl = true;
            //smtpClient.Send(mail);
            try
            {
                Thread email = new Thread(delegate ()
                {
                    smtpClient.Send(mail);
                });
                email.IsBackground = true;
                email.Start();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
        public void SendMailSocialLifeSkill(SocialLifeSkill socialLifeSkill)
        {
            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("hotanminh@hcm.edu.vn");
            mail.To.Add(socialLifeSkill.Email.ToString().Trim());
            mail.CC.Add("hotanminh@hcm.edu.vn");
            mail.Subject = "Hoạt động ngoài giờ - Kĩ năng xã hội, kĩ năng sống";
            mail.IsBodyHtml = true;
            string htmlBody = "<h3>Quý thầy cô vừa đăng ký kĩ năng xã hội, kĩ năng sống thành công.</h3>" +
                "<p>- Mã đăng kí: <b>" + socialLifeSkill.CodeRegisted + "</b></p>" +
                "<p>- Nội dung thực hiện hoạt động: " + socialLifeSkill.ProgramName + "</p>" +
                "<p>- Tên trường: " + socialLifeSkill.School.Name + "</p>" +
                "<p>- Lớp: " + socialLifeSkill.Class.Name + "</p>" +             
                "<p>- Ngày bắt đầu thực hiện: " + socialLifeSkill.DateFrom.Value.Day.ToString("d2") + "/" + socialLifeSkill.DateFrom.Value.Month.ToString("d2") + "/" + socialLifeSkill.DateFrom.Value.Year + "</p>" +
                "<p>- Ngày kết thúc: " + socialLifeSkill.DateTo.Value.Day.ToString("d2") + "/" + socialLifeSkill.DateTo.Value.Month.ToString("d2") + "/" + socialLifeSkill.DateTo.Value.Year + "</p>" +
                "<p>- Tên người phụ trách: " + socialLifeSkill.Creatot + "</p>" +
                "<p>- Số điện thoại liên hệ: " + socialLifeSkill.PhoneNumber + "</p>" +
                "<p>Mọi thắc mắc vui lòng <a href='http://trainghiemthuctien.hcm.edu.vn/lienhe'><i>Liên hệ</i></a></p>";
            mail.Body = htmlBody;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("hotanminh@hcm.edu.vn".Trim(), "Minh1905".Trim());
            smtpClient.EnableSsl = true;
            //smtpClient.Send(mail);
            try
            {
                Thread email = new Thread(delegate ()
                {
                    smtpClient.Send(mail);
                });
                email.IsBackground = true;
                email.Start();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
    }
}