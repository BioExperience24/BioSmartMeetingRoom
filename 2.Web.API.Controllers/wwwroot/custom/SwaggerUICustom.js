var title = document.querySelector('title');
title.innerText = 'PAMA Smart Meeting Room';
console.log("Custom script loaded!");
//DEFAULT USERNAME PASSWORD
var defaultUsername = "";//isi biar gampang debugging doank, klo push jangan lupa dihapus
var defaultPassword = "";

document.addEventListener("DOMContentLoaded", function () {
    setTimeout(() => {
        let swaggerDescription = document.querySelector(".swagger-ui .info .description");

        if (swaggerDescription) {
            // Buat form login
            let loginForm = document.createElement("div");
            loginForm.innerHTML = `
                <div style="padding: 10px; border: 1px solid #ccc; margin: 10px 0; background: #222; color: white;">
                    <h4>🔒 Login API</h4>
                    <input type="text" id="username" placeholder="Username" style="margin-right: 5px; padding:5px;" value="${defaultUsername}" />
                    <input type="password" id="password" placeholder="Password" style="margin-right: 5px; padding:5px;" value="${defaultPassword}" />
                    <button id="swagger-login-btn" style="padding: 5px 10px; cursor: pointer;">Login</button>
                    <div id="token-display" style="margin-top: 10px; padding: 10px; background: #333; color: #fff; display: none; word-wrap: break-word; overflow-wrap: break-word;">
                        <strong>Token:</strong> <span id="token-value" style="word-break: break-all;"></span>
                    </div>
                </div>`;

            // Tambahkan form setelah deskripsi Swagger
            swaggerDescription.appendChild(loginForm);

            // Ambil tombol login setelah form ada
            let loginButton = document.getElementById("swagger-login-btn");

            if (loginButton) {
                loginButton.addEventListener("click", function () {
                    let username = document.getElementById("username").value;
                    let password = document.getElementById("password").value;

                    if (!username || !password) {
                        alert("Username dan Password harus diisi!");
                        return;
                    }
                    // Request token ke API
                    fetch('/api/_Auth/RequestToken', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({ username: username, password: password })
                    })
                        .then(response => response.json())
                        .then(data => {
                            if (data.collection.token) {
                                console.log("Token diterima:", data.collection.token);
                                localStorage.setItem("swagger_token", data.collection.token);

                                // Tampilkan token di halaman
                                let tokenDisplay = document.getElementById("token-display");
                                let tokenValue = document.getElementById("token-value");
                                tokenValue.innerText = data.collection.token;
                                tokenDisplay.style.display = "block";

                                // Tambahkan requestInterceptor untuk otomatis menambahkan header Authorization
                                if (window.ui) {
                                    window.ui.getConfigs().requestInterceptor = (req) => {
                                        if (localStorage.getItem("swagger_token")) {
                                            req.headers.Authorization = "Bearer " + localStorage.getItem("swagger_token");
                                        }
                                        return req;
                                    };
                                }

                                //alert("Login Success");
                            } else {
                                alert("Login Failed");
                            }
                        })
                        .catch(error => {
                            console.error("Error saat login:", error);
                            alert("Terjadi kesalahan! Cek console.");
                        });
                });
            } else {
                console.error("Tombol login tidak ditemukan!");
            }
        } else {
            console.error("Deskripsi Swagger UI tidak ditemukan, pastikan ada elemen dengan class '.info .description'!");
        }
    }, 1000); // Delay agar Swagger UI sempat ter-render
});