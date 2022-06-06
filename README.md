Create testing certificate:


openssl genrsa -des3 -out domain.key 2048 <br />

openssl req -key domain.key -subj '/CN=Jan Kowalski/SN=Kowalski/GN=Jan/O=Testowa firma/C=PL/L=Mazowieckie/serialNumber=NIP-1801908070/description=Jan Kowalski NIP-1801908070' -new -out domain.csr <br />

openssl x509 -signkey domain.key -in domain.csr -req -days 365 -out domain.crt <br />

openssl pkcs12 -inkey domain.key -in domain.crt -export -out domain.pfx <br />
