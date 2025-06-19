async function generatePassword() {
  const length = +document.getElementById("length").value;
  const useUpper = document.getElementById("uppercase").checked;
  const useLower = document.getElementById("lowercase").checked;
  const useNumbers = document.getElementById("numbers").checked;
  const useSymbols = document.getElementById("symbols").checked;

  if (length < 8 || length > 20) {
      alert("Length must be between 8 and 20!");
      return;
  }

  const query = new URLSearchParams({
    length,
    upper: useUpper,
    lower: useLower,
    number: useNumbers,
    symbol: useSymbols
  });

  try {
    const response = await fetch(`http://localhost:5126/generate?${query.toString()}`);
    const data = await response.json();

    if (!response.ok) {
      alert("Error: " + data.error);
      return;
    }

    document.getElementById("password").value = data.password;
    updateStrengthUI(data.strength);
  } catch (err) {
    alert("Failed to connect to password API.");
    console.error(err);
  }
}

function updateStrengthUI(strength) {
  const cpyBtn = document.getElementById("cpy-btn");
  const strengthLabel = document.getElementById("strengthLabel");
  const meterFill = document.getElementById("meterFill");

  let color = "red";
  let width = "20%";

  switch (strength) {
    case "Moderate":
      color = "orange";
      width = "50%";
      break;
    case "Strong":
      color = "green";
      width = "75%";
      break;
    case "Very Strong":
      color = "limegreen";
      width = "100%";
      break;
    default:
      color = "red";
      width = "20%";
  }

  cpyBtn.disabled = false;
  strengthLabel.textContent = strength;
  meterFill.style.width = width;
  meterFill.style.backgroundColor = color;
}

function copyPassword() {
  const passwordField = document.getElementById("password");
  passwordField.select();
  document.execCommand("copy");
  alert("Password copied!");
}