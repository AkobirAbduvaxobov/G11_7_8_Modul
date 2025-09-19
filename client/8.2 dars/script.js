function changeText() {
 
  const name = document.getElementById("fullname");
  const age = document.getElementById("dateOfBirth");
  const result = document.getElementById("result");

  result.innerHTML = `Ism: ${name.value}, Yoshi: ${2025 - age.value}`;
}
