const phoneNumberForm = document.getElementById("phoneNumberForm");
const verificationCodeForm = document.getElementById("verificationCodeForm");
const newPasswordForm = document.getElementById("newPasswordForm");
const submitPhoneNumber = document.getElementById("submitPhoneNumberBtn");

submitPhoneNumber.addEventListener("click", () => SendSms());


function SendSms() {
    const phoneNumber = phoneNumberForm.value;

    if (!phoneNumber) {
        alert("Въведете валиден телефонен номер");
        return;
    }
    const url = `https://localhost:7027/api/sms/send-sms-message/${phoneNumber}`;
    fetch()
}