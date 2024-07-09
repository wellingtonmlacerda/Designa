# Sistema de Designações para Reuniões das Testemunhas de Jeová 
[![NPM](https://img.shields.io/npm/l/react)](https://github.com/wellingtonmlacerda/WLDesigna/blob/master/LICENSE.txt)

## Descrição

Este sistema foi desenvolvido em .NET Core 8.0 com o objetivo de auxiliar na organização e gestão de designações para as reuniões das Testemunhas de Jeová. Ele fornece uma interface amigável e intuitiva para gerenciar diversas atividades e arranjos relacionados às reuniões e ao serviço congregacional.

### Funcionalidades

| Funcionalidade                                                                     | Status |
|---------------------------------------------------------------------------------------|-----|
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

### Tecnologias Utilizadas

- **.NET Core 8.0**
- **C#**
- **SQL Server**
- **Entity Framework Core**
- **HTML/CSS/JavaScript**
- **Docker** (para facilitar a implantação e o gerenciamento de ambientes)
- **Syncfusion** (para componentes avançados de UI)
  - [Como obter a licença Community](https://www.syncfusion.com/products/communitylicense)
- **wppconnect** (para enviar mensagens usando o próprio WhatsApp)

### Instalação e Configuração

# Como executar o projeto
1 - Execute o comando a seguir na janela Package Manager Console:
```bash
Update-Database
```
Ou execute o comando a seguir em Developer PowerShell:
```bash
dotnet ef database update
```
# Clonar repositório
```bash
git clone [https://github.com/wellingtonmlacerda/WLDesigna.git]
```

# Autor

Wellington Marculino de Lacerda

https://www.linkedin.com/in/wellington-lacerda-96798883
