# Introdução

Essa SDK foi construída com o intuito de torná-la flexível, de forma que todos possam utilizar todas as features, de todas as versões de API.

Você pode acessar a documentação oficial do Pagar.me acessando esse [link](https://docs.pagar.me).

## Índice

- [Instalação](#instalação)
- [Configuração](#configuração)
- [Transações](#transações)
  - [Criando uma transação](#criando-uma-transação)
  - [Capturando uma transação](#capturando-uma-transação)
  - [Estornando uma transação](#estornando-uma-transação)
    - [Estornando uma transação parcialmente](#estornando-uma-transação-parcialmente)
    - [Estornando uma transação com split](#estornando-uma-transação-com-split)
  - [Retornando transações](#retornando-transações)
  - [Retornando uma transação](#retornando-uma-transação)
  - [Retornando recebíveis de uma transação](#retornando-recebíveis-de-uma-transação)
  - [Retornando um recebível de uma transação](#retornando-um-recebível-de-uma-transação)
  - [Retornando eventos de uma transação](#retornando-eventos-de-uma-transação)
  - [Testando pagamento de boletos](#testando-pagamento-de-boletos)
- [Cartões](#cartões)
  - [Criando cartões](#criando-cartões)
  - [Retornando cartões](#retornando-cartões)
  - [Retornando um cartão](#retornando-um-cartão)
- [Planos](#planos)
  - [Criando planos](#criando-planos)
  - [Retornando planos](#retornando-planos)
  - [Retornando um plano](#retornando-um-plano)
  - [Atualizando um plano](#atualizando-um-plano)
- [Assinaturas](#assinaturas)
  - [Criando assinaturas](#criando-assinaturas)
  - [Split com assinatura](#split-com-assinatura)
  - [Retornando uma assinatura](#retornando-uma-assinatura)
  - [Retornando assinaturas](#retornando-assinaturas)
  - [Atualizando uma assinatura](#atualizando-uma-assinatura)
  - [Cancelando uma assinatura](#cancelando-uma-assinatura)
  - [Pulando cobranças](#pulando-cobranças)
- [Postbacks](#postbacks)
  - [Retornando postbacks](#retornando-postbacks)
  - [Retornando um postback](#retornando-um-postback)
  - [Reenviando um Postback](#reenviando-um-postback)
- [Saldo do recebedor principal](#saldo-do-recebedor-principal)
- [Operações de saldo](#operações-de-saldo)
  - [Histórico das operações](#histórico-das-operações)
  - [Histórico de uma operação específica](#histórico-de-uma-operação-específica)
- [Recebível](#recebível)
  - [Retornando recebíveis](#retornando-recebíveis)
  - [Retornando um recebível](#retornando-um-recebível)
- [Transferências](#transferências)
  - [Criando uma transferência](#criando-uma-transferência)
  - [Retornando transferências](#retornando-transferências)
  - [Retornando uma transferência](#retornando-uma-transferência)
  - [Cancelando uma transferência](#cancelando-uma-transferência)
- [Antecipações](#antecipações)
  - [Criando uma antecipação](#criando-uma-antecipação)
  - [Obtendo os limites de antecipação](#obtendo-os-limites-de-antecipação)
  - [Confirmando uma antecipação building](#confirmando-uma-antecipação-building)
  - [Cancelando uma antecipação pending](#cancelando-uma-antecipação-pending)
  - [Deletando uma antecipação building](#deletando-uma-antecipação-building)
  - [Retornando antecipações](#retornando-antecipações)
- [Contas bancárias](#contas-bancárias)
  - [Criando uma conta bancária](#criando-uma-conta-bancária)
  - [Retornando uma conta bancária](#retornando-uma-conta-bancária)
  - [Retornando contas bancárias](#retornando-contas-bancárias)
- [Recebedores](#recebedores)
  - [Criando um recebedor](#criando-um-recebedor)
  - [Retornando recebedores](#retornando-recebedores)
  - [Retornando um recebedor](#retornando-um-recebedor)
  - [Atualizando um recebedor](#atualizando-um-recebedor)
  - [Saldo de um recebedor](#saldo-de-um-recebedor)
  - [Operações de saldo de um recebedor](#operações-de-saldo-de-um-recebedor)
  - [Operação de saldo específica de um recebedor](#operação-de-saldo-específica-de-um-recebedor)
- [Clientes](#clientes)
  - [Criando um cliente](#criando-um-cliente)
  - [Retornando clientes](#retornando-clientes)
  - [Retornando um cliente](#retornando-um-cliente)
- [Análises de antifraude](#análises-de-antifraude)
  - [Retornando análises antifraude](#retornando-análises-antifraude)
  - [Retornando uma análise antifraude](#retornando-uma-análise-antifraude)

## Configuração

Para incluir a biblioteca em seu projeto, basta fazer o seguinte:

```C# 
using System;
using PagarMe;
using PagarMe.Base;
using Newtonsoft.Json;
using PagarMe.Model;

PagarMeService.DefaultApiKey = "SUA_CHAVE_API";
PagarMeService.DefaultEncryptionKey = "SUA_cHAVE_ENCRIPTAÇÃO";
```

## Transações

Nesta seção será explicado como utilizar transações no Pagar.me com essa biblioteca.

### Criando uma transação

```C#
Transaction transaction = new Transaction();

transaction.Amount = 2100;
transaction.Card = new Card
{
  Id = "card_cj95mc28g0038cy6ewbwtwwx2"
};

transaction.Customer = new Customer
{
  ExternalId = "#3311",
  Name = "Rick",
  Type = CustomerType.Individual,
  Country = "br",
  Email = "rick@morty.com",
  Documents = new[]
  {
    new Document{
      Type = DocumentType.Cpf,
      Number = "30621143049"
    },
    new Document{
      Type = DocumentType.Cnpj,
      Number = "83134932000154"
    }
  },
  PhoneNumbers = new string[]
  {
    "+5511982738291",
    "+5511829378291"
  },
  Birthday = new DateTime(1991, 12, 12).ToString("yyyy-MM-dd")
};

transaction.Billing = new Billing 
{
  Name = "Morty",
  Address = new Address()
  {
    Country = "br",
    State = "sp",
    City = "Cotia",
    Neighborhood = "Rio Cotia",
    Street = "Rua Matrix",
    StreetNumber = "213",
    Zipcode = "04250000"
  }
};

var Today = DateTime.Now;

transaction.Shipping = new Shipping 
{ 
  Name = "Rick",
  Fee = 100,
  DeliveryDate = Today.AddDays(4).ToString("yyyy-MM-dd"),
  Expedited = false,
  Address = new Address()
  {
    Country = "br",
    State = "sp",
    City = "Cotia",
    Neighborhood = "Rio Cotia",
    Street = "Rua Matrix",
    StreetNumber = "213",
    Zipcode = "04250000"
  }
};

transaction.Item = new[]
{
  new Item()
  {
    Id = "1",
    Title = "Little Car",
    Quantity = 1,
    Tangible = true,
    UnitPrice = 1000
  },
  new Item()
  {
    Id = "2",
    Title = "Baby Crib",
    Quantity = 1,
    Tangible = true,
    UnitPrice = 1000
  }
};

transaction.Save();

Console.Write(transaction.Status);
```

### Capturando uma transação

```C#
var transaction = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO");

transaction.Capture(2000);

Console.Write(transaction.Status);
```

### Estornando uma transação

```C#
var transaction = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO");

transaction.Refund();

Console.Write(transaction.Status)
```

Esta funcionalidade também funciona com estornos parciais, ou estornos com split. Por exemplo:

#### Estornando uma transação parcialmente

```C#
<?php
var transaction = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO");

transaction.Refund(1000);

Console.Write(transaction.Status);
```

#### Estornando uma transação com split

```C#
`EXEMPLO CARTÃO DE CRÉDITO`
var transaction = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO");

var splitRules = new SplitRule[]{
    new SplitRule
    {
        Id = transaction.SplitRules[0].Id,
        Percentage = 20,
        RecipientId = transaction.SplitRules[0].RecipientId,
        ChargeProcessingFee = true
    },
    new SplitRule
    {
        Id = transaction.SplitRules[1].Id,
        Percentage = 80,
        RecipientId = transaction.SplitRules[1].RecipientId,
        ChargeProcessingFee = true
    }
};
try
{
    transaction.RefundWithSplit(VALOR_PARA_SER_ESTORNADO, splitRules);

    Console.Write(transaction.Status);

}
catch (PagarMeException ex)
{
    Console.WriteLine(ex.Error.Errors[0].Message);
}


`EXEMPLO BOLETO`

var account = PagarMeService.GetDefaultService().BankAccounts.Find("ID_DA_CONTA");     
                
var transaction = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO");

var splitRules = new SplitRule[]{
    new SplitRule
    {
        Id = transaction.SplitRules[0].Id,
        Percentage = 20,
        RecipientId = transaction.SplitRules[0].RecipientId,
        ChargeProcessingFee = true
    },
    new SplitRule
    {
        Id = transaction.SplitRules[1].Id,
        Percentage = 80,
        RecipientId = transaction.SplitRules[1].RecipientId,
        ChargeProcessingFee = true
    }
};
try
{
    transaction.RefundWithSplit(VALOR_PARA_SER_ESTORNADO, splitRules, account);

    Console.Write(transaction.Status);
}
catch (PagarMeException ex)
{
    Console.WriteLine(ex.Error.Errors[0].Message);
}

```

### Retornando transações

```C#
var transaction = PagarMeService.GetDefaultService().Transactions.FindAll(new Transaction());

foreach (Transaction trx in transaction)
{
    String JsonObject = JsonConvert.SerializeObject(trx.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

Se necessário, você pode utilizar parâmetros para filtrar essa busca, por exemplo, se quiser filtrar apenas transações pagas, você pode utilizar o código abaixo:

```C#
var status = Tuple.Create("status","paid");

var transactionRequests = PagarMeService.GetDefaultService().Transactions.BuildFindQuery(new Transaction());

transactionRequests.Query.Add(status);

var transaction = PagarMeService.GetDefaultService().Transactions.FinishFindQuery(transactionRequests.Execute());

foreach (Transaction trx in transaction)
{
    String JsonObject = JsonConvert.SerializeObject(trx.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

### Retornando uma transação

```C#
var transaction = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO");

Console.Write(transaction.Status);
```

### Retornando recebíveis de uma transação

```C#
var payables = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO").Payables;
```

### Retornando um recebível de uma transação

```C#
var payable = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO").Payables.Find("ID_PAYABLE");

Console.Write(payable.Status);
```

### Retornando eventos de uma transação

```C#
var events = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO").Events;
```

### Testando pagamento de boletos

```C#
var transaction = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO");
transaction.Status = TransactionStatus.Paid;
transaction.Save();

Console.Write(transaction.Status);
```

## Cartões

Sempre que você faz uma requisição através da nossa API, nós guardamos as informações do portador do cartão, para que, futuramente, você possa utilizá-las em novas cobranças, ou até mesmo implementar features como one-click-buy.

### Criando cartões

```C#
Card card = new Card();
card.Number = "4018720572598048";
card.HolderName = "Aardvark Silva";
card.ExpirationDate = "1122";
card.Cvv = "123";

card.Save();

Console.Write(card.Id); 
```

### Retornando cartões

```C#
var card = PagarMeService.GetDefaultService().Cards.FindAll(new Card());

foreach (Card cards in card)
{
    String JsonObject = JsonConvert.SerializeObject(cards.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}

```

### Retornando um cartão

```C#
var card = PagarMeService.GetDefaultService().Cards.Find("ID_CARTÃO");

Console.Write(card.Id);
```

## Planos

Representa uma configuração de recorrência a qual um cliente consegue assinar.
É a entidade que define o preço, nome e periodicidade da recorrência

### Criando planos

```C#
Plan plan = new Plan
{
    Name = "The Pro Plan - Platinum  - Best Ever",
    Amount = 15000,
    Days = 30
};

plan.Save();

Console.Write(plan.id);
```

### Retornando planos

```C#
var plan = PagarMeService.GetDefaultService().Plans.FindAll(new Plan());

foreach (Plan plans in plan)
{
    String JsonObject = JsonConvert.SerializeObject(plans.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

### Retornando um plano

```C#
var plan = PagarMeService.GetDefaultService().Plans.Find("ID_PLANO");

Console.Write(plan.id);
```

### Atualizando um plano

```C#
var plan = PagarMeService.GetDefaultService().Plans.Find("ID_PLANO");
plan.Name = "The Pro Plan - Susan";
plan.TrialDays = 7;
plan.Save();

Console.Write(plan.id);
```

## Assinaturas

### Criando assinaturas

```C#
Subscription subscription = new Subscription
{
    PaymentMethod = PaymentMethod.CreditCard,
    CardNumber = "4901720080344448",
    CardHolderName = "Jose da Silva",
    CardExpirationDate = "1021",
    CardCvv = "123"
};
subscription.Plan = PagarMeService.GetDefaultService().Plans.Find(ID_PLANO);
Customer customer = new Customer
{
    Name = "John Appleseed",
    DocumentNumber = "92545278157",
    Email = "jappleseed@apple.com",
    Address = new Address
    {
        Street = "Rua Dr. Geraldo Campos Moreira",
        Neighborhood = "Cidade Monções",
        Zipcode = "04571020",
        StreetNumber = "240"
    },
    Phone = new Phone
    {
        Ddd = "11",
        Number = "988774455"
    }
};

subscription.Customer = customer;

subscription.Save();

Console.Write(subscription.Status);
```

### Split com assinatura

```C#
Subscription subscription = new Subscription
{
    PaymentMethod = PaymentMethod.CreditCard,
    CardNumber = "4901720080344448",
    CardHolderName = "Jose da Silva",
    CardExpirationDate = "1021",
    CardCvv = "123"
};
subscription.Plan = PagarMeService.GetDefaultService().Plans.Find(ID_PLANO);
Customer customer = new Customer
{
    Name = "John Appleseed",
    DocumentNumber = "92545278157",
    Email = "jappleseed@apple.com",
    Address = new Address
    {
        Street = "Rua Dr. Geraldo Campos Moreira",
        Neighborhood = "Cidade Monções",
        Zipcode = "04571020",
        StreetNumber = "240"
    },
    Phone = new Phone
    {
        Ddd = "11",
        Number = "988774455"
    }
};

var split = new SplitRule[]
{

    new SplitRule
    {
        RecipientId = "ID_RECEBEDOR",
        ChargeProcessingFee = true,
        Liable = true,
        Percentage = 20
    },
    new SplitRule
    {
        RecipientId = "ID_RECEBEDOR",
        ChargeProcessingFee = true,
        Liable = true,
        Percentage = 80
    }

};

subscription.SplitRules = split;

subscription.Customer = customer;

subscription.Save();

Console.Write(subscription.Status);
```

### Retornando uma assinatura

```C#
var subscription = PagarMeService.GetDefaultService().Subscriptions.Find("ID_ASSINATURA")

Console.Write(subscription.Status);
```

### Retornando assinaturas

```C#
var subscription = PagarMeService.GetDefaultService().Subscriptions.FindAll(new Subscription());

foreach (Subscription subscriptions in subscription)
{
    String JsonObject = JsonConvert.SerializeObject(subscriptions.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

Se necessário, você pode aplicar filtros em sua busca. Por exemplo, se quiser trazer todas as assinatura de um certo plano, você pode utilizar o código abaixo:

```C#

```

### Atualizando uma assinatura

```C#
subscription.PaymentMethod = PaymentMethod.CreditCard;
subscription.Card = PagarMeService.GetDefaultService().Cards.Find("ID_CARTÃO");
subscription.Plan = PagarMeService.GetDefaultService().Plans.Find(ID_PLANO);
subscription.Save();

Console.Write(subscription.Status);
```

### Cancelando uma assinatura

```C#
var subscription = PagarMeService.GetDefaultService().Subscriptions.Find("ID_ASSINATURA");

subscription.Cancel();

Console.Write(subscription.Status);
```

### Pulando cobranças

```C#
var subscription = PagarMeService.GetDefaultService().Subscriptions.Find("ID_ASSINATURA");

subscription.SettleCharges();

Console.Write(subscription.Status);
```

## Postbacks

Ao criar uma transação ou uma assinatura você tem a opção de passar o parâmetro postback_url na requisição. Essa é uma URL do seu sistema que irá então receber notificações a cada alteração de status dessas transações/assinaturas.

Para obter informações sobre postbacks, 3 informações serão necessárias, sendo elas: `model`, `model_id` e `postback_id`.

`model`: Se refere ao objeto que gerou aquele POSTback. Pode ser preenchido com o valor `transaction` ou `subscription`.

`model_id`: Se refere ao ID do objeto que gerou ao POSTback, ou seja, é o ID da transação ou assinatura que você quer acessar os POSTbacks.

`postback_id`: Se refere à notificação específica. Para cada mudança de status de uma assinatura ou transação, é gerado um POSTback. Cada POSTback pode ter várias tentativas de entregas, que podem ser identificadas pelo campo `deliveries`, e o ID dessas tentativas possui o prefixo `pd_`. O campo que deve ser enviado neste parâmetro é o ID do POSTback, que deve ser identificado pelo prefixo `po_`. 

### Retornando postbacks

```C#
var postback = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO").Postbacks.FindAll(new Postback());

foreach (Postback postbacks in postback)
{
    String JsonObject = JsonConvert.SerializeObject(postbacks.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

### Retornando um postback

```C#
var postback = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO").Postbacks.Find("ID_POSTBACK");

Console.WriteLine(postback.Payload);
```

### Reenviando um postback

```C#
var postback = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO").Postbacks.Find("ID_POSTBACK");

postback.Redeliver();
```

## Saldo do recebedor principal

Para saber o saldo de sua conta, você pode utilizar esse código:

```C#
var balance = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR_PRINCIPAL").Balance;

String JsonObject = JsonConvert.SerializeObject(balance.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

## Operações de saldo

Com este objeto você pode acompanhar todas as movimentações financeiras ocorridas em sua conta Pagar.me.

### Histórico das operações

```C#
var balanceOperation = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR").Balance.Operations.FindAll(new BalanceOperation());

foreach (BalanceOperation balanceOperations in balanceOperation)
{
    String JsonObject = JsonConvert.SerializeObject(balanceOperations.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

Se necessário, você pode passar filtros como parâmetro, por exemplo:

```C#
var status = Tuple.Create("status","available");

var balanceOperationRequests = PagarMeService.GetDefaultService().Balance.Operations.BuildFindQuery(new BalanceOperation());

balanceOperationRequests.Query.Add(status);

var balanceOperation = PagarMeService.GetDefaultService().Balance.Operations.FinishFindQuery(balanceOperationRequests.Execute());

foreach (BalanceOperation balanceOperations in balanceOperation)
{
    String JsonObject = JsonConvert.SerializeObject(balanceOperations.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```


### Histórico de uma operação específica

```C#
var balanceOperations = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR").Balance.Operations.find("ID_BALANCE_OPERATION");

String JsonObject = JsonConvert.SerializeObject(balanceOperations.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

## Recebível

Objeto contendo os dados de um recebível. O recebível (payable) é gerado automaticamente após uma transação ser paga. Para cada parcela de uma transação é gerado um recebível, que também pode ser dividido por recebedor (no caso de um split ter sido feito).

### Retornando recebíveis

```C#
var transactionPayables = PagarMeService.GetDefaultService().Payables.FindAll(new Payable());

foreach (Payable payables in transactionPayables)
{
    String JsonObject = JsonConvert.SerializeObject(payables.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

Se necessário, você pode aplicar filtros na busca dos payables, por exemplo, você pode recuperar todos os payables de uma transação:

```C#
<?php
$transactionPayables = $pagarme->payables()->getList([
    'transaction_id' => 'ID_DA_TRANSAÇÃO'
]);
```

### Retornando um recebível

```C#
var payable = PagarMeService.GetDefaultService().Payables.Find("ID_RECEBÍVEL");

String JsonObject = JsonConvert.SerializeObject(balanceOperations.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

## Transferências
Transferências representam os saques de sua conta.

### Criando uma transferência
```C#
var transfer = new Transfer {
  Amount = 1000	
};

transfer.Save();

String JsonObject = JsonConvert.SerializeObject(transfer.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Retornando transferências

```C#
var transfer = PagarMeService.GetDefaultService().Transfers.FindAll(new Transfer());

foreach (Transfer transfers in transfer)
{
    String JsonObject = JsonConvert.SerializeObject(transfers.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

Se necessário, você pode aplicar filtros em sua busca, por exemplo:

```C#
var status = Tuple.Create("status","pending_transfer");

var transfersRequests = PagarMeService.GetDefaultService().Transfers.BuildFindQuery(new Transfer());

transfersRequests.Query.Add(status);

var transfer = PagarMeService.GetDefaultService().Transfers.FinishFindQuery(transfersRequests.Execute());

foreach (Transfer transfers in transfer)
{
    String JsonObject = JsonConvert.SerializeObject(transfers.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

### Retornando uma transferência

```C#
var transfers = PagarMeService.GetDefaultService ().Transfers.Find ("ID_TRANSFERÊNCIA");

String JsonObject = JsonConvert.SerializeObject(transfers.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Cancelando uma transferência

```C#
var transfers = PagarMeService.GetDefaultService().Transfers.Find("ID_TRANSFERÊNCIA");

transfers.CancelTransfer();

String JsonObject = JsonConvert.SerializeObject(transfers.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

## Antecipações

Para entender o que são as antecipações, você deve acessar esse [link](https://docs.pagar.me/docs/overview-antecipacao).

### Criando uma antecipação

```C#
var recipient = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR");

var bulkAnticipation = new BulkAnticipation()
{
    Timeframe = TimeFrame.Start,
    PaymentDate = DateTime.Today.AddDays(3),
    RequestedAmount = 561599,
    Build = true
};

recipient.CreateAnticipation(bulkAnticipation);

String JsonObject = JsonConvert.SerializeObject(recipient.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Obtendo os limites de antecipação

```C#
var recipient = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR");
var MaxLimit = recipient.MaxAnticipationValue(TimeFrame.Start, DateTime.Today.AddDays(3));
var MinLimit = recipient.MinAnticipationValue(TimeFrame.Start, DateTime.Today.AddDays(3));

String JsonObject = JsonConvert.SerializeObject(recipient.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Confirmando uma antecipação building

```C#
var recipient = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR");

var anticipation = recipient.Anticipations.Find("ID_ANTECIPAÇÃO");

recipient.ConfirmAnticipation(anticipation);

String JsonObject = JsonConvert.SerializeObject(recipient.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Cancelando uma antecipação pending

```C#
var recipient = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR");

var anticipation = recipient.Anticipations.Find("ID_ANTECIPAÇÃO");

recipient.CancelAnticipation(anticipation);

String JsonObject = JsonConvert.SerializeObject(recipient.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Deletando uma antecipação building

```C#
var recipient = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR");
var anticipation = recipient.Anticipations.Find("ID_ANTECIPAÇÃO");
recipient.DeleteAnticipation(anticipation);

String JsonObject = JsonConvert.SerializeObject(recipient.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Retornando antecipações

```C#
var recipient = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR");
var anticipations = recipient.Anticipations.FindAll(new BulkAnticipation());

foreach (BulkAnticipation ba in anticipations)
{
    String JsonObject = JsonConvert.SerializeObject(ba.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

Se necessário, você pode aplicar filtros nessa busca, por exemplo, pelo valor antecipado:

```C#
var status = Tuple.Create("amount","65000");

var recipient = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR");

var antecipationsRequests = recipient.Anticipations.BuildFindQuery(new BulkAnticipation());

antecipationsRequests.Query.Add(status);

var antecipation = recipient.Anticipations.FinishFindQuery(antecipationsRequests.Execute());

foreach (BulkAnticipation ba in antecipation)
{
    String JsonObject = JsonConvert.SerializeObject(ba.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

## Contas bancárias

Contas bancárias identificam para onde será enviado o dinheiro de futuros pagamentos.

### Criando uma conta bancária

```C#
BankAccount bank = new BankAccount();
bank.Agencia = "0192";
bank.AgenciaDv = "0";
bank.Conta = "03245";
bank.ContaDv = "0";
bank.BankCode = "341";
bank.DocumentNumber = "26268738888";
bank.LegalName = "API BANK ACCOUNT";
bank.Save();

String JsonObject = JsonConvert.SerializeObject(bank.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Retornando uma conta bancária

```C#
var banks = PagarMeService.GetDefaultService().BankAccounts.Find("ID_CONTA_BANCÁRIA");

String JsonObject = JsonConvert.SerializeObject(banks.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);

```

### Retornando contas bancárias

```C#
var accounts = PagarMeService.GetDefaultService().BankAccounts.FindAll(new BankAccount());

foreach (BankAccount banks in accounts)
{
    String JsonObject = JsonConvert.SerializeObject(banks.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

## Recebedores

Para dividir uma transação entre várias entidades, é necessário ter um recebedor para cada uma dessas entidades. Recebedores contém informações da conta bancária para onde o dinheiro será enviado, e possuem outras informações para saber quanto pode ser antecipado por ele, ou quando o dinheiro de sua conta será sacado automaticamente.

### Criando um recebedor

```C#
Recipient recipient = new Recipient();
recipient.TransferInterval = TransferInterval.Weekly;
recipient.TransferDay = 5;
recipient.TransferEnabled = true;
recipient.BankAccount = PagarMeService.GetDefaultService().BankAccounts.Find("ID_CONTA_BANCÁRIA");
recipient.Save();

String JsonObject = JsonConvert.SerializeObject(recipient.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Retornando recebedores

```C#
var recipient = PagarMeService.GetDefaultService().Recipients.FindAll(new Recipient());

foreach (Recipient recipients in recipient)
{
    String JsonObject = JsonConvert.SerializeObject(recipients.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

Se necessário, você pode aplicar filtros nessa busca. Por exemplo, se quiser retornar todos os recebedores, com transferência habilitada, você pode utilizar esse código:

```C#
var transferInterval = Tuple.Create("transfer_interval","weekly");

var recipientRequests = PagarMeService.GetDefaultService().Recipients.BuildFindQuery(new Transaction());

recipientRequests.Query.Add(transferInterval);

var recipient = PagarMeService.GetDefaultService().Recipients.FinishFindQuery(recipientRequests.Execute());

foreach (Recipient recipients in recipient)
{
    String JsonObject = JsonConvert.SerializeObject(recipients.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

### Retornando um recebedor

```C#
var recipient = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR");

String JsonObject = JsonConvert.SerializeObject(recipient.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Atualizando um recebedor

```C#
var recipient = new Recipient()
{
  Id = "ID_RECEBEDOR",
  TransferInterval = TransferInterval.Monthly,
  TransferDay = 15,
  TransferEnabled = true,
  AnticipatableVolumePercentage = 50,
  BankAccount = new BankAccount()
  {
    Id = "ID_CONTA_BANCÁRIA"
  }
};
recipient.Save();

String JsonObject = JsonConvert.SerializeObject(recipient.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Saldo de um recebedor

```C#
var recipient = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR");
var available = recipient.Balance.Available;
var waitingFunds = recipient.Balance.WaitingFunds;
var transferred = recipient.Balance.Transferred;

Console.WriteLine("Saldo disponível: " + available);
Console.WriteLine("Saldo pendente: " + waitingFunds);
Console.WriteLine("Saldo sacado: " + transferred);
```

### Operações de saldo de um recebedor

```C#
var balanceOperation = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR").Balance.Operations.FindAll(new BalanceOperation());

foreach (BalanceOperation balanceOperations in balanceOperation)
{
    String JsonObject = JsonConvert.SerializeObject(balanceOperations.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

### Operação de saldo específica de um recebedor

```C#
var balanceOperations = PagarMeService.GetDefaultService().Recipients.Find("ID_RECEBEDOR").Balance.Operations.Find("ID_BALANCE_OPERATION");

String JsonObject = JsonConvert.SerializeObject(balanceOperations.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

## Clientes

Clientes representam os usuários de sua loja, ou negócio. Este objeto contém informações sobre eles, como nome, e-mail e telefone, além de outros campos.

### Criando um cliente

```C#
var Customer = new Customer
 {
	ExternalId = "#3311",
	Name = "Rick",
	Type = CustomerType.Individual,
	Country = "br",
	Email = "rick@morty.com",
	Documents = new[]
	{
		new Document{
				Type = DocumentType.Cpf,
				Number = "11111111111"
		},
		new Document{
			Type = DocumentType.Cnpj,
			Number = "83134932000154"
		}
	},
	PhoneNumbers = new string[]
	{
		"+5511982738291",
		"+5511829378291"
	},
	Birthday = new DateTime(1991, 12, 12).ToString("yyyy-MM-dd")
};
Customer.Save();

String JsonObject = JsonConvert.SerializeObject(Customer.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

### Retornando clientes

```C#
var customer = PagarMeService.GetDefaultService().Customers.FindAll(new Customer());

foreach (Customer customers in customer)
{
    String JsonObject = JsonConvert.SerializeObject(customers.ToDictionary(SerializationType.Full));

    Console.WriteLine(JsonObject);
}
```

### Retornando um cliente

```C#
var customers = PagarMeService.GetDefaultService().Customers.Find("ID_CLIENTE");

String JsonObject = JsonConvert.SerializeObject(customers.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```

## Análise de Antifraude

As análises do antifraude, que ocorrem posteriormente à autorização da adquirente, são baseadas em predição por machine learning. Por serem análises estatísticas, elas podem apresentar falsos positivos e falsos negativos. Ou seja, existe a possibilidade de que algumas transações saudáveis sejam sinalizadas como tentativas de fraude – falsos positivos – e de que algumas transações fraudulentas sejam sinalizadas como idôneas – falsos negativos.

Você pode ver mais sobre a Revisão Manual clicando [aqui](https://docs.pagar.me/page/revis%C3%A3o-manual).

### Retornando análises antifraude

```C#
var antifraudAnalisis = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO").AntifraudAnalisis.FindAll(new AntifraudAnalisis());

foreach (AntifraudAnalisis analisis in antifraudAnalisis)
{
  String JsonObject = JsonConvert.SerializeObject(analisis.ToDictionary(SerializationType.Full));

  Console.WriteLine(JsonObject);
}
```

### Retornando uma análise antifraude

```C#
var antifraudAnalysis = PagarMeService.GetDefaultService().Transactions.Find("ID_TRANSAÇÃO").AntifraudAnalysis.Find("ID_ANALISE_ANTIFRAUDE");

String JsonObject = JsonConvert.SerializeObject(antifraudAnalisis.ToDictionary(SerializationType.Full));

Console.WriteLine(JsonObject);
```


## License

Check [here](LICENSE).
