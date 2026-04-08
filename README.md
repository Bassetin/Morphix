# 🎮 Morphix

**Morphix** é um jogo de plataforma 2D desenvolvido na Unity com foco em **arquitetura de código, sistemas reutilizáveis e design orientado a mecânicas**.

Este projeto foi criado como parte do meu portfólio para demonstrar habilidades em:

* Programação com C#
* Arquitetura de sistemas em jogos
* Design de mecânicas
* Boas práticas de desenvolvimento na Unity

---

## 🧠 Visão Geral

Morphix é um jogo de precisão onde o jogador controla uma entidade capaz de **alternar entre diferentes formas físicas**, cada uma impactando diretamente o comportamento do personagem.

O design do jogo explora:

* Controle responsivo
* Física aplicada ao gameplay
* Tomada de decisão em tempo real

---

## ⚙️ Destaques Técnicos

* 🧩 **Arquitetura baseada em abstração**

  * Uso de classe base (`PlayerBase`) para padronizar comportamento
  * Facilita extensão e manutenção

* 🔄 **Sistema de Formas**

  * Implementação modular para adicionar novas formas
  * Cada forma altera atributos como velocidade, peso e interação física

* 🎮 **Sistema de Movimento Customizado**

  * Controle refinado de velocidade, pulo e gravidade
  * Estruturado para fácil adaptação (teclado → gamepad via Input System)

* 🧱 **Separação de Responsabilidades**

  * Código organizado visando legibilidade e escalabilidade

---

## 🕹️ Mecânicas Principais

* Alternância entre formas com propriedades únicas
* Plataforma com foco em precisão e timing
* Interação com obstáculos baseada em física

---

## 🧪 Desafios Técnicos Enfrentados

* Estruturação de um sistema flexível para múltiplas formas
* Balanceamento entre física realista e jogabilidade responsiva
* Preparação do sistema de input para suportar diferentes dispositivos

---

## 🚀 Tecnologias Utilizadas

* **Unity (2D)**
* **C#**

---

## 📂 Estrutura do Projeto

```id="proj-struct"
Assets/
├── Scripts/
│   ├── Player/
│   │   ├── PlayerBase.cs
│   │   └── (implementações específicas)
│   ├── Systems/
│   └── Utils/
├── Scenes/
├── Prefabs/
└── Art/
```

---

## 📈 Status do Projeto

🔨 Em desenvolvimento ativo

* [x] Sistema base de movimentação
* [x] Arquitetura inicial de formas
* [ ] Sistema completo de troca de forma
* [ ] Level design (protótipos)
* [ ] Integração com Input System
* [ ] UI e feedback visual
---

## 🧭 Roadmap

* Expansão do sistema de formas
* Implementação de habilidades únicas por forma
* Refinamento de física e controle
* Criação de níveis progressivos
* Sistema de feedback visual e sonoro

---

## 💡 Aprendizados

Durante o desenvolvimento deste projeto, estou aprofundando conhecimentos em:

* Arquitetura de software para jogos
* Padrões de projeto aplicados à Unity
* Iteração de gameplay baseada em testes

---

## 🤝 Contato

Se quiser conversar sobre o projeto ou oportunidades:

* LinkedIn: *https://www.linkedin.com/in/lucasrodrigodev/*
* Email: *Bassetolucas.dev@gmail.com*

---

## 📄 Licença

MIT License
