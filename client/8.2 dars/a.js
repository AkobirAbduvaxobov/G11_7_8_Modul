document.addEventListener("DOMContentLoaded", function () {
  const signupForm = document.getElementById("signupForm");
  const signinForm = document.getElementById("signinForm");

  if (signupForm) {
    debugger;
    signupForm.addEventListener("submit", function (e) {
      e.preventDefault();
      const data = Object.fromEntries(new FormData(signupForm));
      console.log("Signup Data:", data);
      alert("Signup success!");
    });
  }

  if (signinForm) {
    signinForm.addEventListener("submit", function (e) {
      e.preventDefault();
      const data = Object.fromEntries(new FormData(signinForm));
      console.log("Signin Data:", data);
      alert("Signin success!");
    });
  }
});