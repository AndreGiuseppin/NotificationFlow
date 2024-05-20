- Link da arquitetura no MIRO: https://miro.com/app/board/uXjVKHxQEKY=/?share_link_id=557407757589
![image](https://github.com/AndreGiuseppin/NotificationFlow/assets/27440524/a8cc4264-77e9-4471-99cc-6221b255c6c2)

- Mapeamento do database no Draw.io: 
![image](https://github.com/AndreGiuseppin/NotificationFlow/assets/27440524/13c19b56-cb5a-4b47-b2ac-c44a15b1cd14)

PENSAMENTO SOBRE ARQUITETURA
- Pensei em um fluxo asincrono para adicionar a notificação pelo motivo de ser possivel enviar varias notificações rapidamente, e caso ocorra certa demora para atribuir as notificações aos usuarios, isso não impactaria o processo.
- Separei as preferencias em gerais e especificas, assim o usuario poderá escolher habilitar ou desabilitar elas, o mesmo iria ocorrer quando tivesse push, email e sms, teriamos mais 3 tipos de preferencias.
- Utilizei o Hangfire como agendador de tarefa.

OBS.: A ideia era deixar a API no docker compose tambem, mas tive alguns problemas na etapa que faço uma conexão ao banco de dados para criar a tabela HangFire. 

POSSIVEIS ERROS AO EXECUTAR:
- Se der erro na etapa de conectar ao banco no momento de criação da database hangfire, restartar a aplicação. Aqui como é feito uma conexão ao banco na startup, as vezes ocorre esse erro relacionado ao login
- Para a primeira vez que iniciar o Kafka via docker, caso a notificação não chegue, restartar a aplicação para o kafka identificar que tem mensagem. A partir daqui não é necessario mais restartar. 

COMO RODAR A APLICAÇÃO
 - git clone https://github.com/AndreGiuseppin/NotificationFlow.git
 - Rodar o docker
 - run docker compose up na pasta que existe o docker-compose.yml
 - Criar 2 usuarios via API
 - Adicionar notificação

COMO ADICIONAR NOTIFICAÇÃO
- PARA NOTIFICAÇÃO ESPECIFICA SEM AGENDAMENTO:
-- Deixar a flag IsGeneral = false, adicionar o userId no campo solicitado e remover a propriedade scheduleTime

- PARA NOTIFICAÇÃO ESPECIFICA COM AGENDAMENTO:
-- Deixar a flag IsGeneral = false, adicionar o userId no campo solicitado e adicionar data UTC na propriedade scheduleTime

- PARA NOTIFICAÇÃO GERAL SEM AGENDAMENTO:
-- Deixar a flag IsGeneral = true, deixar o userId como 0 e remover a propriedade scheduleTime

- PARA NOTIFICAÇÃO GERAL COM AGENDAMENTO:
-- Deixar a flag IsGeneral = true, deixar o userId como 0 e adicionar data UTC na propriedade scheduleTime

NA ROTA api/user/{id}/notification-preferences É POSSIVEL HABILITAR OU DESABILITAR A FLAG DE PREFERENCIAS DE NOTIFICAÇÃO

Qualquer problema ou duvidas, podem entrar em contato: andre.giuseppin@gmail.com
