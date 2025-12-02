#  SUPER RPG WPF / API

Projeto de cadastro de **Personagens** e **Itens** para um RPG, utilizando **.NET + Entity Framework Core + SQLite**.  
Permite criar personagens, vincular itens a eles, listar, salvar e remover.

---

##  Passos para rodar
1. **Clonar o repositório**
```bash
git clone https://github.com/seuusuario/super-rpg-wpf.git
cd super-rpg-wpf
 ```
2. **Instalar dependências**
```bash
dotnet restore
```
3. **Criar o banco e aplicar migrations**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
4. **Rodas o projeto**
```bash
dotnet run
```

# Entidades
- Personagem (ID, nome, classe, nível)

- Item (ID, nome, raridade, preço, personagemId, personagem)

##  Rotas da API

Caso você exponha o projeto como **Web API** (controllers REST), estas seriam as rotas:

### Personagens
- `GET /api/personagens` → lista todos os personagens
- `GET /api/personagens/{id}` → retorna um personagem específico
- `POST /api/personagens` → cria um novo personagem
- `PUT /api/personagens/{id}` → atualiza um personagem existente
- `DELETE /api/personagens/{id}` → remove um personagem e seus itens

### Itens
- `GET /api/itens` → lista todos os itens
- `GET /api/itens/{id}` → retorna um item específico
- `POST /api/itens` → cria um novo item vinculado a um personagem
- `PUT /api/itens/{id}` → atualiza um item
- `DELETE /api/itens` → remove um item

##  Exemplo de requisição

### Criar Personagem
```http
POST http://localhost:5000/api/personagens
Content-Type: application/json

{
  "nome": "Aragorn",
  "classe": "Guerreiro",
  "nivel": 10
}
```
## Listar personagens
```http
GET http://localhost:5000/api/personagens
```

##  Como testar

### Usando Postman
1. Abra o **Postman**.
2. Crie uma nova requisição para cada rota (`GET`, `POST`, `PUT`, `DELETE`).
3. Configure o **Content-Type: application/json** para requisições `POST` e `PUT`.
4. Envie os exemplos de requisições mostrados acima.
5. Verifique as respostas retornadas pela API.

### Usando arquivos `.http` (VS Code / Rider)
1. Crie um arquivo `test.http` na raiz do projeto.
2. Cole os exemplos de requisições (GET, POST, PUT, DELETE).
3. Clique em **Send Request** (VS Code com a extensão REST Client).
4. Veja a resposta diretamente no editor.
