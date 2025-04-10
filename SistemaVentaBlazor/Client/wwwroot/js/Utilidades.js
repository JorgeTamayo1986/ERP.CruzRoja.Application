function DescargarArchivo(nombre,base64) {

    const link = document.createElement("a");
    link.download = nombre;
  /*  link.href = "data:application/octet-stream;base64," + base64;*/
    /*link.href = "data:application/vnd.ms-excel;base64," + base64;*/
    link.href = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + base64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}



window.encryptData = async function (plainText, key) {
    const enc = new TextEncoder();
    const data = enc.encode(plainText);
    const encKey = await window.crypto.subtle.importKey(
        "raw",
        new TextEncoder().encode(key),
        { name: "AES-GCM" },
        false,
        ["encrypt", "decrypt"]
    );
    const iv = window.crypto.getRandomValues(new Uint8Array(12)); // 12-byte random IV
    const encryptedData = await window.crypto.subtle.encrypt(
        { name: "AES-GCM", iv: iv },
        encKey,
        data
    );

    // Combine iv + encrypted data to store
    const result = new Uint8Array(iv.byteLength + encryptedData.byteLength);
    result.set(iv);
    result.set(new Uint8Array(encryptedData), iv.byteLength);

    return btoa(String.fromCharCode.apply(null, result)); // Convert to base64 string
};

window.decryptData = async function (cipherText, key) {
    const encryptedData = new Uint8Array(atob(cipherText).split("").map(c => c.charCodeAt(0)));
    const iv = encryptedData.slice(0, 12);
    const data = encryptedData.slice(12);

    const encKey = await window.crypto.subtle.importKey(
        "raw",
        new TextEncoder().encode(key),
        { name: "AES-GCM" },
        false,
        ["encrypt", "decrypt"]
    );

    const decryptedData = await window.crypto.subtle.decrypt(
        { name: "AES-GCM", iv: iv },
        encKey,
        data
    );

    const dec = new TextDecoder();
    return dec.decode(decryptedData);
};