# Sistema de Designações para Reuniões das Testemunhas de Jeová 
[![NPM](https://img.shields.io/npm/l/react)](https://github.com/wellingtonmlacerda/WLDesigna/blob/master/LICENSE.txt)

## Descrição

Este sistema foi desenvolvido em .NET Core 8.0 com o objetivo de auxiliar na organização e gestão de designações para as reuniões das Testemunhas de Jeová. Ele fornece uma interface amigável e intuitiva para gerenciar diversas atividades e arranjos relacionados às reuniões e ao serviço congregacional.

### Funcionalidades

| Funcionalidade                                                                     | Status |
|---------------------------------------------------------------------------------------|-----|
| **Publicadores**                                                                      |     |
| - Cadastro de publicadores.                                                           | ✔️ |
| **Designações da Escola na Reunião "Nossa Vida e Ministério Cristão"**                |     |
| - Permite atribuir discursos, leituras e demais tarefas da escola bíblica.            | ✔️ |
| - Facilita a gestão das designações semanais e mensais.                               | ✔️ |
| - Notifica os responsáveis pela parte.                                                | ❌ |
| **Arranjo de Limpeza da Congregação**                                                 |     |
| - Organiza os turnos de limpeza para manter o Salão do Reino em perfeitas condições.  | ❌ |
| - Notifica os responsáveis pelos arranjos de limpeza.                                 | ❌ |
| **Coordenação de Serviço**                                                            | ❌ |
| - Ajuda a organizar e designar volantes, indicadores, som e Zoom.                     | ❌ |
| - Mantém um registro das designações e da participação dos irmãos.                    | ❌ | 
| **Reunião de Fim de Semana**                                                          | ❌ |
| - Facilita a gestão das designações para as reuniões de fim de semana.                | ❌ |
| - Gera pautas e ordens de serviço para os oradores e demais participantes.            | ❌ |
| **Arranjo para Pegar Idosos**                                                         | ❌ |
| - Organiza voluntários para pegar e levar idosos às reuniões.                         | ❌ |
| - Garante que todos os que necessitam de transporte sejam assistidos.                 | ❌ |

### Capturas de Tela

#### Tela Principal - Apostila da Reunião Vida e Ministério

Esta é a tela principal que mostra as apostilas das reuniões de meio de semana. Os botões levam para a página de reunião daquela apostila, onde estão as partes e designações dos irmãos.

![Tela Principal - Apostila da Reunião Vida e Ministério](https://github.com/wellingtonmlacerda/Designa/blob/master/Designa/wwwroot/capturas/Tela_Principal.png)

#### Tela da Reunião - Designações

Esta é a tela da reunião para uma apostila específica, mostrando as partes e as designações dos irmãos.

![Tela da Reunião - Designações](https://github.com/wellingtonmlacerda/Designa/blob/master/Designa/wwwroot/capturas/Reunioes.gif)

#### Tela de Cadastro de Publicadores

Esta é a tela de cadastro de publicadores, onde você pode adicionar e gerenciar os publicadores da congregação.

![Tela de Cadastro de Publicadores](https://github.com/wellingtonmlacerda/Designa/blob/master/Designa/wwwroot/capturas/Publicadores.png)

### Tecnologias Utilizadas

- **.NET Core 8.0**
- **C#**
- **SQLite**
- **Entity Framework Core**
- **HTML/CSS/JavaScript**
- **Docker** (para facilitar a implantação e o gerenciamento de ambientes)
- **Syncfusion** (para componentes avançados de UI)
  - [Como obter a licença Community](https://www.syncfusion.com/products/communitylicense)
- **wppconnect** (para enviar mensagens usando o próprio WhatsApp)

### Instalação e Configuração

#### Como executar o projeto
1 - Execute o comando a seguir na janela Package Manager Console:
```bash
Update-Database
```
Ou execute o comando a seguir em Developer PowerShell:
```bash
dotnet ef database update
```
## Clonar repositório
```bash
git clone [https://github.com/wellingtonmlacerda/WLDesigna.git]
```
### Contribuição

Contribuições são bem-vindas! Por favor, abra uma *issue* para discutir as mudanças que você gostaria de fazer. Sinta-se à vontade para enviar *pull requests*.

## Autor

Wellington Marculino de Lacerda

https://www.linkedin.com/in/wellington-lacerda-96798883
