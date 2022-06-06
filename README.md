Create testing certificate:

1.
openssl genrsa -des3 -out domain.key 2048
2.
openssl req -key domain.key -subj '/CN=Jan Kowalski/SN=Kowalski/GN=Jan/O=Testowa firma/C=PL/L=Mazowieckie/serialNumber=NIP-1801908070/description=Jan Kowalski NIP-1801908070' -new -out domain.csr
3.
openssl x509 -signkey domain.key -in domain.csr -req -days 365 -out domain.crt
4.
openssl pkcs12 -inkey domain.key -in domain.crt -export -out domain.pfx
