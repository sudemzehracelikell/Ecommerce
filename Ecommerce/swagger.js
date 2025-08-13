// backend/swagger.js
const swaggerJSDoc = require("swagger-jsdoc");

const options = {
    definition: {
        openapi: "3.0.0",
        info: {
            title: "E-Ticaret API",
            version: "1.0.0",
            description: "Ürünler, markalar, kategoriler, kullanıcılar API'si",
        },
        servers: [
            {
                url: "http://localhost:5083",
            },
        ],
    },
    apis: ["./routes/*.js"], // Swagger açıklamaları bu dosyalarda olacak
};

const swaggerSpec = swaggerJSDoc(options);
module.exports = swaggerSpec;
