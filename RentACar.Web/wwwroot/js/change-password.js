const phoneNumberForm = document.getElementById("phoneNumberForm");
const verificationCodeForm = document.getElementById("verificationCodeForm");
const newPasswordForm = document.getElementById("newPasswordForm");
const submitPhoneNumber = document.getElementById("submitPhoneNumberBtn");

submitPhoneNumber.addEventListener("click");


function SendSms() {
    const phoneNumber = phoneNumberForm.value;

    if (!phoneNumber) {
        alert("Въведете валиден телефонен номер");
        return;
    }


}