document.addEventListener("DOMContentLoaded", () => {
	const CalculateAge = date => {
		if (date != null) {
			const now = new Date(Date.now());
			let age = now.getFullYear() - date.getFullYear();
			const monthsDifference = (now.getMonth() + 1) - (date.getMonth() + 1);
			if (monthsDifference < 0 || (monthsDifference === 0 && now.getDate() < date.getDate()))
				age--;
			return age;
		}
		else return 0;
	};
	{
		const btnGetPremium = document.getElementById("btnGetPremium"), lblRequestingCalculation = document.getElementById("lblRequestingCalculation"), slcPeriod = document.getElementById("slcPeriod"), slcPlan = document.getElementById("slcPlan"),
			slcState = document.getElementById("slcState"), tblResults = document.getElementById("tblResults"), txtAge = document.getElementById("txtAge"), txtDateOfBirth = document.getElementById("txtDateOfBirth");
		const RequestCalculation = () => {
			const age = txtAge.valueAsNumber, dateOfBirth = txtDateOfBirth.valueAsDate, period = slcPeriod.value, plan = slcPlan.value, state = slcState.value, tbody = tblResults.querySelector("tbody"), xhr = new XMLHttpRequest();
			const request = {
				Plan: { Name: plan },
				State: { Code: state },
				MonthOfBirth: dateOfBirth.getMonth() + 1,
				Age: age,
				Period: period
			};
			const requestRaw = JSON.stringify(request);
			SetControlsEnability(false);
			lblRequestingCalculation.style.display = "initial";
			tblResults.style.display = "none";
			tbody.innerHTML = "";
			xhr.onreadystatechange = () => {
				if (xhr.readyState == XMLHttpRequest.DONE) {
					const status = xhr.status;
					SetControlsEnability(true);
					lblRequestingCalculation.style.display = "none";
					if (status != 0) {
						const dataRaw = xhr.response;
						const data = JSON.parse(dataRaw);
						for (const item of data) {
							const tdAnnual = document.createElement("td"), tdCarrier = document.createElement("td"), tdMonthly = document.createElement("td"), tdPremium = document.createElement("td"), tr = document.createElement("tr");
							const FillCell = (cell, value, type = "number") => {
								const input = document.createElement("input");
								input.type = type;
								input.readOnly = true;
								input.value = value;
								cell.appendChild(input);
							};
							FillCell(tdCarrier, item.carrier, "text");
							FillCell(tdPremium, item.premium);
							FillCell(tdAnnual, (item.premium * (12 / period)).toFixed(2));
							FillCell(tdMonthly, (item.premium / period).toFixed(2));
							tr.appendChild(tdCarrier);
							tr.appendChild(tdPremium);
							tr.appendChild(tdAnnual);
							tr.appendChild(tdMonthly);
							tbody.appendChild(tr);
						}
						tblResults.style.display = "initial";
					}
					else alert("Cannot connect to the Tranzact Premium Calculator service. Please try again later.")
				}
			};
			xhr.open("POST", "/Calculator/Calculate");
			xhr.setRequestHeader("Content-Type", "application/json");
			xhr.send(requestRaw);
		};
		const SetControlsEnability = isEnabled => {
			txtDateOfBirth.disabled = !isEnabled;
			slcState.disabled = !isEnabled;
			slcPlan.disabled = !isEnabled;
			txtAge.disabled = !isEnabled;
			btnGetPremium.disabled = !isEnabled;
			slcPeriod.disabled = !isEnabled;
		};
		txtDateOfBirth.oninput = () => {
			const date = txtDateOfBirth.valueAsDate;
			if (date != null) {
				const age = CalculateAge(date);
				txtAge.value = age;
			}
		};
		btnGetPremium.onclick = () => {
			if (txtDateOfBirth.reportValidity())
				RequestCalculation();
		};
		slcPeriod.onchange = () => {
			if (txtDateOfBirth.checkValidity())
				RequestCalculation();
		};
	}
});