# Desafio Raro Labs

## Criação de uma WebApi em .Net 5 que consome um serviço de consulta de cep.

- O Projeto foi desenvolvido em .Net 5;
- Implementação com a preocupação com o CleanCode;
- Retorno do resultante em Json;
- Comentário relevantes tentando abordar todos os pontos de implementação;
- Há injeção de dependencias, utilizando uma camada de serviço para fazer o consumo do serviço de consulta de cep;
- Uso de versionamento de Api, utilizando a biblioteca de versionamento da Microsoft.AspNET.MVC;
- Configuração do Swagger de forma simples, para documentar a utilização do serviço.

## Utilização da webapi após executa-la

- O verbo/operação/método utilizado foi o GET por se tratar de um retorno de um Json contendo dados de um CEP válido;
- Há três possíveis status codes utilizados nesta implementação;
-- 200 - Trazendo dados do CEP válido;
-- 404 - Quando o cep não foi encontrado;
-- 400 - Quando o cep tem formato divergente do esperado;
- Endpoint do webapi: http://localhost:5000/api/v1/Cep/consulta/[cep válido | apenas números]
-- Ex.: http://localhost:5000/api/v1/Cep/consulta/69094390

## é isso. =)