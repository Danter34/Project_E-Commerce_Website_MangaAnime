
//Sự kiện kiểm tra mật khẩu có đúng định dạng không, nếu sai báo lỗi
passwordFieldLogin.addEventListener("change", function () {
  checkPassword(errorPassLogin, passwordFieldLogin);
});

//Kiểm tra định dạng của password
function checkPassword(error, field) {
  var pass = field.value;
  if (!isPasswordCorrect(pass)) {
    error.innerHTML =
      "Mật khẩu phải có ít nhất 8 ký tự, 1 chữ cái, 1 chữ số, một ký tự đặc biệt";
    error.parentNode.style.marginBottom = "60px";
  } else {
    error.innerHTML = "";
    error.parentNode.style.marginBottom = "30px";
  }
}
//Kiểm tra mật khẩu đúng định dạng
function isPasswordCorrect(pass) {
  var regex = /^(?=.*[a-zA-Z])(?=.*\d)(?=.*[!#$%&?@ "]).{8,}$/;
  return regex.test(pass);
}

//Tham chiếu đến các thẻ
const showHidePasswordLogin = document.getElementById("show-hide-pass-login");
const loginLink = document.getElementById("login-link");
const registerLink = document.getElementById("register-link");
const showHidePasswordRegister = document.getElementById(
  "show-hide-pass-register"
);
const showHidePasswordRegisterAgain = document.getElementById(
  "show-hide-pass-register-again"
);
const passwordFieldRegister = document.querySelector(
  '.register-form .input-box input[type="password"]'
);
const passwordFieldAgainRegister = document.getElementById(
  "input-password-again"
);
const accountFieldRegister = document.getElementById("account-field-register");

const errorPassRegister = document.getElementById("error-pass-register");
const errorPassAgainRegister = document.getElementById(
  "error-pass-again-register"
);
const btnRegister = document.getElementById("btn-register");

//Sự kiện kích hoạt lật thẻ
// End sự kiện kích hoạt lật thẻ

//tạo sự kiện cho show/hide password
showHidePasswordLogin.addEventListener("click", function () {
  // thay đổi thẻ và ẩn/hiện text
  if (showHidePasswordLogin.classList.contains("fa-lock")) {
    //Hiện text
    passwordFieldLogin.type = "text";
    showHidePasswordLogin.classList.remove("fa-lock");
    showHidePasswordLogin.classList.add("fa-unlock");
  } else {
    //ẩn text
    passwordFieldLogin.type = "password";
    showHidePasswordLogin.classList.remove("fa-unlock");
    showHidePasswordLogin.classList.add("fa-lock");
  }
});
showHidePasswordRegister.addEventListener("click", function () {
  // thay đổi thẻ và ẩn/hiện text
  if (showHidePasswordRegister.classList.contains("fa-lock")) {
    // hiện text
    passwordFieldRegister.type = "text";
    showHidePasswordRegister.classList.remove("fa-lock");
    showHidePasswordRegister.classList.add("fa-unlock");
  } else {
    // ẩn text
    passwordFieldRegister.type = "password";
    showHidePasswordRegister.classList.remove("fa-unlock");
    showHidePasswordRegister.classList.add("fa-lock");
  }
});
showHidePasswordRegisterAgain.addEventListener("click", function () {
  // thay đổi thẻ và ẩn/ hiện text
  if (showHidePasswordRegisterAgain.classList.contains("fa-lock")) {
    //hiện text
    passwordFieldAgainRegister.type = "text";
    showHidePasswordRegisterAgain.classList.remove("fa-lock");
    showHidePasswordRegisterAgain.classList.add("fa-unlock");
  } else {
    //Ẩn text
    passwordFieldAgainRegister.type = "password";
    showHidePasswordRegisterAgain.classList.remove("fa-unlock");
    showHidePasswordRegisterAgain.classList.add("fa-lock");
  }
});
//End tạo sự kiện cho show/hide password

//Gán sự kiện check sau khi nhập đk tài khoản

//Gán sự kiện check sau khi mật khẩu đã được nhập có đúng định dạng ko
passwordFieldRegister.addEventListener("change", function () {
  checkPassword(errorPassRegister, passwordFieldRegister);
  checkPasswordMatches(passwordFieldRegister, passwordFieldAgainRegister);
});
passwordFieldAgainRegister.addEventListener("change", function () {
  checkPassword(errorPassAgainRegister, passwordFieldAgainRegister);
  checkPasswordMatches(passwordFieldRegister, passwordFieldAgainRegister);
});