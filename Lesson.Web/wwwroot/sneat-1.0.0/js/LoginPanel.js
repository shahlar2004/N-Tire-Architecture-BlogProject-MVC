$(".message a").click(function () {
    $("form").animate({ height: "toggle", opacity: "toggle" }, "slow");
    document.querySelector(".reset-password").style.display = "none";
    document.querySelector(".hide-loginbtn").style.display = "none";
});


document.querySelector(".hidesignin").addEventListener("click", function () {
    document.querySelector(".hide-loginbtn").style.display = "block";
});

const showHide = document.querySelectorAll(".show-hide i");
const password = document.querySelectorAll(".pas-con");

showHide.forEach((Element) => {
    Element.addEventListener("click", function () {
        this.classList.toggle("fa-eye-slash");
        this.classList.toggle("fa-eye");

        password.forEach((Element) => {
            const type =
                Element.getAttribute("type") === "password" ? "text" : "password";
            Element.setAttribute("type", type);
        });
    });
});

//userCreate--------------->

document.querySelector("#createUser").addEventListener("click", function () {
    var createInpust = document.querySelectorAll(".register-form input"),
        say = 0;

    createInpust.forEach((item) => {
        if (item.value.trim() == "") {
            say++;
        }
    });

    if (say != 0) {
        alert("Məlumatları tam doldurun");
        return;
    }

    var myPersonObj = {
        personEmail: document.querySelector("#personEmail").value.trim(),
        personName: document.querySelector("#personName").value.trim(),
        personNumber: document.querySelector("#personNumber").value.trim(),
        personPassword: document.querySelector("#personPassword").value.trim(),
        personSurname: document.querySelector("#personSurname").value.trim(),
        personUserName: document.querySelector("#personUsername").value.trim(),
    };

    // console.log(myPersonObj);

    createInpust.forEach((item) => {
        item.value = "";
    });

    fetchData(myPersonObj);
});

async function fetchData(obj) {
    var data,
        say2 = 0;
    // console.log("Hos geldiniz")
    try {
        const response = await fetch("https://localhost:7290/api/FirstApi");

        if (!response.ok) {
            console.error("Request failed with status:", response.status);
            return;
        }

        data = await response.json();
    } catch (error) {
        console.error("Error fetching data:", error.message);
    }
    data.forEach((item) => {
        console.log(say2);
        if (item.personNumber.trim() == obj.personNumber.trim()) {
            say2++;
            console.log(say2);
            console.log(item);
            console.log(obj);
            alert("Bu nömrə ilə qeydiyyat mövcuddur");
            return;
        }

        if (item.personEmail.trim() == obj.personEmail.trim()) {
            say2++;
            console.log(say2);
            console.log(item);
            console.log(obj);
            alert("Bu email ilə qeydiyyat mövcuddur");
        }

        if (item.personUserName.trim() == obj.personUserName.trim()) {
            say2++;
            console.log(say2);
            console.log(item);
            console.log(obj);
            alert("Bu istifadəçi adı  ilə qeydiyyat mövcuddur");
        }
    });

    if (say2 == 0) {
        fetch("https://localhost:7290/api/FirstApi", {
            method: "Post",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(obj),
        });

        alert("Qeydiyyat uğurla tamamlandı");
    }
    console.log(say2);
    say2 = 0;
}

const login = document.querySelector("#loginbutton");

login.addEventListener("click", function () {
    const userName = document.querySelector("#userName").value;
    const password = document.querySelector("#password").value;
    let number = 0;

    if (userName.trim() != "") {
        if (password.trim() != "") {
            /*
            i have to fetch api
            */

        } else {
            alert("it's empty");
        }
    }

    else {
        alert("it's empty");
    }
});

const changePassword = document.querySelector("#changepassword a");

changePassword.addEventListener("click", function () {
    document.querySelector(".login-form").style.display = "none";
    document.querySelector(".reset-password").style.display = "block";
    document.querySelector(".button-p").style.display = "none";
});

const resetPassword = document.querySelector("#resetPassword");

resetPassword.addEventListener("click", function () {
    const myEmail = document.querySelector("#myEmailAddres").value;
    const personPassword = document.querySelector("#newPassword").value;
    var id = 0;

    console.log(myEmail);



    fetch("https://localhost:7290/api/FirstApi/1", {
        method: "PUT",
        headers: {
            "Content-Type": "text/plain",
        },
        body: personPassword,
    })
        .then(alert("Parolunuzda dəyişiklik edildi!"))
        .catch("Something's warning");

    // fetch("https://localhost:7290/api/FirstApi")
    //   .then((response) => response.json())
    //   .then((data) => {
    //     for (var i = 0; i < data.length; i++) {
    //       if (data[i].personEmail == myEmail.trim()) {
    //         id = data[i].personId;
    //         putData(id, newPassword);
    //       }
    //     }

    //     if (id == 0) {
    //       alert("Daxil etdiyiniz email doğru deyil");
    //     }
    //     console.log(data);
    //   })
    //   .catch((error) => console.log(error));
});

// function putData(id, newPass) {
//   fetch("https://localhost:7290/api/FirstApi/" + id, {
//     method: "PUT",
//     headers: {
//       "Content-Type": "text/plain",
//     },
//     body: newPass,
//   })
//     .then(alert("Parolunuzda dəyişiklik edildi!"))
//     .catch("Something's warning");
// }
