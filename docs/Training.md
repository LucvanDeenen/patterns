# Design Patterns training

## Quick Reference by Use Case

| Pattern | Use Cases |
|---------|-----------|
| [Factory](#factory) | Database connections, UI element creation, document/file type handling, plugin/driver instantiation |
| [Abstract Factory](#abstract-factory) | Cross-platform UI frameworks, theme systems, database systems with different vendors, payment gateway integrations |
| [Builder](#builder) | Complex object creation (HTTP requests, SQL queries), UI component configuration, document builders, configuration objects |
| [Prototype](#prototype) | Undo/redo functionality, cloning game entities, copying complex configuration objects, creating similar objects with variations |
| [Singleton](#singleton) | Database connections, logging systems, configuration managers, thread pools, caching systems |
| [Adapter](#adapter) | Integrating legacy systems with new code, library compatibility, hardware drivers, third-party API integration |
| [Bridge](#bridge) | UI abstractions across different platforms, graphics rendering systems, database abstraction layers |
| [Composite](#composite) | File systems, UI component hierarchies, organizational structures, menu systems, document structures (DOM) |
| [Decorator](#decorator) | I/O streams, GUI components enhancement, text formatting, logging wrappers, feature toggles |
| [Facade](#facade) | API wrappers, library abstractions, subsystem simplification, framework integration, third-party service integration |
| [Flyweight](#flyweight) | Text rendering systems, game object pooling, cache systems, large datasets with repeating values, texture management |
| [Proxy](#proxy) | Lazy loading, access control, logging/auditing, caching, remote objects (RPC) |
| [Chain of Responsibility](#chain-of-responsibility) | Event handling systems, approval workflows, logging level handling, HTTP middleware, exception handling |
| [Command](#command) | Undo/redo functionality, task queuing, macro recording, transaction systems, remote control systems |
| [Interpreter](#interpreter) | Expression parsers, query languages, configuration file parsers, rule engines, SQL parsers |
| [Iterator](#iterator) | Collection traversal, tree traversal, different iteration strategies, lazy evaluation, cursor-based access |
| [Mediator](#mediator) | Chat applications, air traffic control, GUI component coordination, event aggregation, dialog boxes |
| [Memento](#memento) | Undo/redo systems, save game functionality, transaction rollback, snapshot storage, change history |
| [Observer](#observer) | Event listeners, MVC architectures, property change notifications, pub/sub systems, reactive programming |
| [State](#state) | State machines, workflow engines, game states, document states, connection states |
| [Strategy](#strategy) | Payment methods, sorting algorithms, compression algorithms, route planning, authentication methods |
| [Template](#template) | Framework base classes, document generation, data processing pipelines, algorithm patterns, database access patterns |
| [Visitor](#visitor) | Compiler AST traversal, report generation, serialization/deserialization, optimization passes, document processing |

---

## Pattern levels
- Idioms, low level patterns, python (EAFP, Easier to Ask for Forgiveness than Permission) uses this a lot (e.g. best practice force cast in Python).

- High levels, MVC

--- 
## OOP / SOLID
[Big OOPs](https://www.youtube.com/watch?v=wo84LFzx5nI)
Core is, Classes > Objects > States.

OOP 4 pillars;
1. **Abstraction**, Modelling of object (plane for booking.com vs flight simulator)
2. **Polymorphism**, Different instances of the same type (rectangle, circle > shape)
3. **Encapsulation**, private > getters, setters
4. **Inheritance**, Car > StationWagon

- Composition over Inheritance, composition pattern > Gameobject example where instead of multiple interface implementations we rely on constructing the based methods (delegate like pattern)

---
## Design Patterns (Gang of Four)
### Creational
Used for instantiating objects.

#### Factory
  - ELI5: Imagine a toy factory. Instead of making toys yourself, you ask the factory "I want a robot" and it builds it for you.
  - Use Cases: Database connections, UI element creation, document/file type handling, plugin/driver instantiation
  - Description: Decouple the creation and instantiation of objects by relying a (static) initializer and the products that it sets up. Where we rely on the interface that are used on the products.
  - Example:
    ```
    interface Animal { makeSound() }
    class Dog implements Animal {
      makeSound() { return "Woof!" }
      fetch() { return "Fetching ball..." }
    }
    class Cat implements Animal {
      makeSound() { return "Meow!" }
      scratch() { return "Scratching furniture..." }
    }
    
    class AnimalFactory {
      static createAnimal(type) {
        switch(type) {
          case "dog": return new Dog()
          case "cat": return new Cat()
          default: throw new Error("Unknown animal")
        }
      }
    }
    
    // Usage
    animal = AnimalFactory.createAnimal("dog")
    print(animal.makeSound())  // "Woof!"
    ```


#### [Abstract Factory](../AbstractFactoryPattern/README.md)
  - ELI5: Like having different factories for different countries. A Chinese factory makes things differently than a German factory, but both make the same products.
  - Use Cases: Cross-platform UI frameworks, theme systems, database systems with different vendors, payment gateway integrations
  - Description: Similar to the factory but this interfaces the factory to give implementations based on an another initializer, initialization stage.
  - Example:
    ```
    interface Button { click() }
    interface Checkbox { toggle() }
    
    class WindowsButton implements Button {
      click() { return "Windows button clicked" }
    }
    class MacButton implements Button {
      click() { return "Mac button clicked" }
    }
    
    interface UIFactory {
      createButton(): Button
      createCheckbox(): Checkbox
    }
    
    class WindowsUIFactory implements UIFactory {
      createButton() { return new WindowsButton() }
      createCheckbox() { return new WindowsCheckbox() }
    }
    
    class MacUIFactory implements UIFactory {
      createButton() { return new MacButton() }
      createCheckbox() { return new MacCheckbox() }
    }
    
    // Usage
    factory = getPlatformFactory()  // Returns appropriate factory
    button = factory.createButton()
    ```


#### Builder
  - ELI5: Building a sandwich step-by-step: first the bread, then cheese, then ham, then lettuce. You don't dump everything in at once.
  - Use Cases: Complex object creation (HTTP requests, SQL queries), UI component configuration, document builders, configuration objects
  - Description: Construction of code is done by a builder subclass which introduces the initializers of the properties as opposed to exposing the Getters, Setters. Director is sometimes included to determine order of operations.
  - Example:
    ```
    class House {
      private walls, roof, doors, windows
      
      constructor(builder) {
        this.walls = builder.walls
        this.roof = builder.roof
        this.doors = builder.doors
        this.windows = builder.windows
      }
      
      describe() {
        return `House with ${this.walls} walls, ${this.doors} doors, ${this.windows} windows`
      }
    }
    
    class HouseBuilder {
      addWalls(count) { this.walls = count; return this }
      addRoof(type) { this.roof = type; return this }
      addDoors(count) { this.doors = count; return this }
      addWindows(count) { this.windows = count; return this }
      build() { return new House(this) }
    }
    
    // Usage
    house = new HouseBuilder()
      .addWalls(4)
      .addRoof("tile")
      .addDoors(2)
      .addWindows(8)
      .build()
    print(house.describe())
    ```

#### Prototype
  - ELI5: You have a perfect drawing. Instead of drawing it again from scratch, you photocopy it.
  - Use Cases: Undo/redo functionality, cloning game entities, copying complex configuration objects, creating similar objects with variations
  - Description: Creates a copy (not reference) of an existing object.
  - Example:
    ```
    class Shape {
      constructor(color) { this.color = color }
      clone() {
        newShape = new this.constructor(this.color)
        newShape.x = this.x
        newShape.y = this.y
        return newShape
      }
    }
    
    class Circle extends Shape {
      constructor(color, radius) {
        super(color)
        this.radius = radius
      }
      clone() {
        cloned = super.clone()
        cloned.radius = this.radius
        return cloned
      }
    }
    
    // Usage
    original = new Circle("red", 5)
    copy = original.clone()
    copy.radius = 10  // Only affects the copy, not original
    print(original.radius)  // Still 5
    ```

#### [Singleton](../SingletonPattern/README.md)
  - ELI5: There's only one president in a country. Everyone talks to the same president, not different presidents.
  - Use Cases: Database connections, logging systems, configuration managers, thread pools, caching systems
  - Description: Ensure a single class instance of an object that is used / returned on usage.
  - Example:
    ```
    class Database {
      private static instance = null
      private connection
      
      private constructor() {
        this.connection = this.establishConnection()
      }
      
      static getInstance() {
        if (Database.instance == null) {
          Database.instance = new Database()
        }
        return Database.instance
      }
      
      establishConnection() {
        return "Connected to database"
      }
      
      query(sql) {
        return `Executing: ${sql}`
      }
    }
    
    // Usage
    db1 = Database.getInstance()
    db2 = Database.getInstance()
    print(db1 === db2)  // true - same object
    print(db1.query("SELECT * FROM users"))
    ```

### Structural
Explains how to assemble objects and classes in larger structures. For example how they are related.

#### [Adapter](../AdapterPattern/README.md)
  - ELI5: A USB adapter lets you plug an old connector into a new USB port. It translates between the two different types.
  - Use Cases: Integrating legacy systems with new code, library compatibility, hardware drivers, third-party API integration
  - Description: Allows incompatible interfaces to collaborate (wrapper).
  - Example:
    ```
    // Old system with different interface
    class LegacyPaymentSystem {
      makeTransaction(cardNumber, amount) {
        return `Processing ${amount} on ${cardNumber}`
      }
    }
    
    // New system expects different interface
    interface ModernPaymentProcessor {
      processPayment(payment: PaymentData)
    }
    
    // Adapter bridges the gap
    class PaymentSystemAdapter implements ModernPaymentProcessor {
      private legacySystem = new LegacyPaymentSystem()
      
      processPayment(payment) {
        // Translate new interface to old interface
        return this.legacySystem.makeTransaction(
          payment.card,
          payment.amount
        )
      }
    }
    
    // Usage
    processor = new PaymentSystemAdapter()
    result = processor.processPayment({card: "1234", amount: 99.99})
    ```

#### Bridge
  - ELI5: A remote control is separate from the TV. You can have different remotes (abstraction) and different TVs (implementation) that work together.
  - Use Cases: UI abstractions across different platforms, graphics rendering systems, database abstraction layers
  - Description: Decouples an abstraction from its implementation by creating an independent hierarchy, allowing them to vary separately.
  - Example:
    ```
    // Implementation hierarchy
    interface Device {
      turnOn()
      turnOff()
      setChannel(channel)
    }
    
    class TV implements Device {
      turnOn() { return "TV is on" }
      turnOff() { return "TV is off" }
      setChannel(ch) { return `Channel set to ${ch}` }
    }
    
    class Radio implements Device {
      turnOn() { return "Radio is on" }
      turnOff() { return "Radio is off" }
      setChannel(ch) { return `Frequency set to ${ch}` }
    }
    
    // Abstraction hierarchy
    class RemoteControl {
      protected device: Device
      constructor(device) { this.device = device }
      togglePower() { return this.device.turnOn() }
    }
    
    class AdvancedRemote extends RemoteControl {
      mute() { return "Muted" }
      setChannel(ch) { return this.device.setChannel(ch) }
    }
    
    // Usage
    tv = new TV()
    remote = new AdvancedRemote(tv)
    print(remote.setChannel(5))
    ```

#### Composite
  - ELI5: A folder can contain files OR other folders. You can treat a single file and a folder full of files the same way.
  - Use Cases: File systems, UI component hierarchies, organizational structures, menu systems, document structures (DOM) 
  - Description: Composes objects into tree structures to represent part-whole hierarchies, allowing clients to treat individual objects and compositions uniformly.
  - Example:
    ```
    interface FileSystemComponent {
      getSize(): int
      display(indent: string)
    }
    
    class File implements FileSystemComponent {
      constructor(name, size) { this.name = name; this.size = size }
      getSize() { return this.size }
      display(indent = "") { print(indent + "File: " + this.name) }
    }
    
    class Folder implements FileSystemComponent {
      children: List<FileSystemComponent> = []
      constructor(name) { this.name = name }
      
      add(component) { this.children.push(component) }
      
      getSize() {
        total = 0
        for (child in this.children) {
          total += child.getSize()
        }
        return total
      }
      
      display(indent = "") {
        print(indent + "Folder: " + this.name)
        for (child in this.children) {
          child.display(indent + "  ")
        }
      }
    }
    
    // Usage
    root = new Folder("root")
    documents = new Folder("documents")
    documents.add(new File("resume.pdf", 100))
    documents.add(new File("letter.doc", 50))
    root.add(documents)
    root.display()
    print("Total size: " + root.getSize())
    ```

#### Decorator
  - ELI5: A plain coffee is a coffee. Add whipped cream, it's still a coffee but fancier. Add chocolate syrup too, even fancier. You keep adding stuff without changing the coffee itself.
  - Use Cases: I/O streams, GUI components enhancement, text formatting, logging wrappers, feature toggles
  - Description: Attaches additional responsibilities to an object dynamically, providing a flexible alternative to subclassing.
  - Example:
    ```
    interface Coffee {
      cost(): float
      description(): string
    }
    
    class SimpleCoffee implements Coffee {
      cost() { return 2.0 }
      description() { return "Simple coffee" }
    }
    
    // Decorators
    class CoffeeDecorator implements Coffee {
      protected coffee: Coffee
      constructor(coffee) { this.coffee = coffee }
    }
    
    class MilkDecorator extends CoffeeDecorator {
      cost() { return this.coffee.cost() + 0.5 }
      description() { return this.coffee.description() + ", milk" }
    }
    
    class ChocolateDecorator extends CoffeeDecorator {
      cost() { return this.coffee.cost() + 1.0 }
      description() { return this.coffee.description() + ", chocolate" }
    }
    
    class WhippedCreamDecorator extends CoffeeDecorator {
      cost() { return this.coffee.cost() + 0.75 }
      description() { return this.coffee.description() + ", whipped cream" }
    }
    
    // Usage
    coffee = new SimpleCoffee()
    coffee = new MilkDecorator(coffee)
    coffee = new ChocolateDecorator(coffee)
    coffee = new WhippedCreamDecorator(coffee)
    print(coffee.description())  // "Simple coffee, milk, chocolate, whipped cream"
    print(coffee.cost())  // 4.25
    ```

#### Facade
  - ELI5: A car has many complex systems inside (engine, transmission, etc.) but you just press the gas pedal. The car hides all the complexity.
  - Use Cases: API wrappers, library abstractions, subsystem simplification, framework integration, third-party service integration
  - Description: Provides a unified, simplified interface to a set of interfaces in a subsystem.
  - Example:
    ```
    // Complex subsystems
    class RoomService {
      findAvailableRoom(dates) { return "Room 101" }
      reserveRoom(room) { return `${room} reserved` }
    }
    
    class PaymentService {
      processPayment(amount) { return `Charged ${amount}` }
    }
    
    class NotificationService {
      sendConfirmation(email) { return `Sent to ${email}` }
    }
    
    // Facade that hides complexity
    class HotelReservationFacade {
      private rooms = new RoomService()
      private payment = new PaymentService()
      private notify = new NotificationService()
      
      bookRoom(dates, email, amount) {
        room = this.rooms.findAvailableRoom(dates)
        this.rooms.reserveRoom(room)
        this.payment.processPayment(amount)
        this.notify.sendConfirmation(email)
        return `Booking complete for ${room}`
      }
    }
    
    // Usage - client only deals with facade
    facade = new HotelReservationFacade()
    result = facade.bookRoom(["2024-01-01", "2024-01-05"], "user@email.com", 500)
    ```

#### Flyweight
  - ELI5: Instead of each character in a book having its own font object, all 'A's share one font object. Saves memory!
  - Use Cases: Text rendering systems, game object pooling, cache systems, large datasets with repeating values, texture management
  - Description: Uses sharing to support large numbers of fine-grained objects efficiently by sharing common state.
  - Example:
    ```
    class FontFlyweight {
      constructor(family, size, bold) {
        this.family = family
        this.size = size
        this.bold = bold
      }
    }
    
    class FontFactory {
      private static fonts = {}
      
      static getFont(family, size, bold) {
        key = `${family}-${size}-${bold}`
        if (FontFactory.fonts[key] == null) {
          FontFactory.fonts[key] = new FontFlyweight(family, size, bold)
        }
        return FontFactory.fonts[key]
      }
    }
    
    class Character {
      constructor(char, x, y, fontKey) {
        this.char = char
        this.x = x
        this.y = y
        this.font = FontFactory.getFont(...fontKey)  // shared
      }
      
      render() {
        return `${this.char} at (${this.x},${this.y}) in ${this.font.family}`
      }
    }
    
    // Usage
    char1 = new Character('A', 0, 0, ['Arial', 12, false])
    char2 = new Character('A', 10, 0, ['Arial', 12, false])
    print(char1.font === char2.font)  // true - same font object
    ```

#### Proxy
  - ELI5: You call your assistant instead of the CEO directly. Your assistant controls who gets to talk to the CEO and what messages get through.
  - Use Cases: Lazy loading, access control, logging/auditing, caching, remote objects (RPC)
  - Description: Provides a surrogate or placeholder for another object to control access to it.
  - Example:
    ```
    interface Subject {
      request()
    }
    
    class RealSubject implements Subject {
      request() {
        // expensive operation
        return "RealSubject: Handling request"
      }
    }
    
    class AccessControlProxy implements Subject {
      private realSubject: RealSubject
      private accessLevel
      
      constructor(accessLevel) {
        this.accessLevel = accessLevel
      }
      
      request() {
        if (this.accessLevel >= 5) {
          if (this.realSubject == null) {
            this.realSubject = new RealSubject()
          }
          return this.realSubject.request()
        }
        return "Access denied"
      }
    }
    
    // Usage
    proxy = new AccessControlProxy(3)
    print(proxy.request())  // "Access denied"
    
    admin_proxy = new AccessControlProxy(10)
    print(admin_proxy.request())  // Creates and calls RealSubject
    ```


### Behavioral
How to provide loose coupling and how the different objects communicate.

#### Chain of Responsibility
  - ELI5: A customer complaint goes to the cashier. If they can't fix it, it goes to the manager. If the manager can't, it goes to the director.
  - Use Cases: Event handling systems, approval workflows, logging level handling, HTTP middleware, exception handling
  - Description: Passes a request along a chain of handlers where each handler decides whether to process it or pass it along.
  - Example:
    ```
    class Request {
      constructor(amount) { this.amount = amount }
    }
    
    abstract class Approver {
      protected next: Approver
      
      setNext(next) { this.next = next; return next }
      
      abstract canApprove(request)
      
      handle(request) {
        if (this.canApprove(request)) {
          return `${this.constructor.name} approved ${request.amount}`
        }
        if (this.next != null) {
          return this.next.handle(request)
        }
        return "Request denied"
      }
    }
    
    class Manager extends Approver {
      canApprove(request) { return request.amount <= 1000 }
    }
    
    class Director extends Approver {
      canApprove(request) { return request.amount <= 10000 }
    }
    
    class CEO extends Approver {
      canApprove(request) { return true }
    }
    
    // Setup chain
    manager = new Manager()
    director = new Director()
    ceo = new CEO()
    manager.setNext(director).setNext(ceo)
    
    // Usage
    print(manager.handle(new Request(500)))   // Manager approved
    print(manager.handle(new Request(5000)))  // Director approved
    print(manager.handle(new Request(50000))) // CEO approved
    ```

#### Command
  - ELI5: Instead of directly telling the waiter "bring me coffee", you write it on a note (command object). The waiter can execute it later or multiple people can do it.
  - Use Cases: Undo/redo functionality, task queuing, macro recording, transaction systems, remote control systems
  - Description: Encapsulates a request as an object, allowing parameterization of clients with different requests and queuing of requests.
  - Example:
    ```
    interface Command {
      execute()
      undo()
    }
    
    class Light {
      constructor(location) { this.location = location }
      on() { return `${this.location} light on` }
      off() { return `${this.location} light off` }
    }
    
    class LightOnCommand implements Command {
      constructor(light) { this.light = light }
      execute() { return this.light.on() }
      undo() { return this.light.off() }
    }
    
    class LightOffCommand implements Command {
      constructor(light) { this.light = light }
      execute() { return this.light.off() }
      undo() { return this.light.on() }
    }
    
    class RemoteControl {
      private commands = []
      private history = []
      
      setCommand(command) { this.commands.push(command) }
      pressButton(index) {
        this.commands[index].execute()
        this.history.push(this.commands[index])
      }
      undo() {
        if (this.history.length > 0) {
          this.history.pop().undo()
        }
      }
    }
    
    // Usage
    remote = new RemoteControl()
    light = new Light("Living Room")
    remote.setCommand(new LightOnCommand(light))
    remote.setCommand(new LightOffCommand(light))
    remote.pressButton(0)  // Light on
    remote.undo()          // Light off
    ```

#### Interpreter
  - ELI5: Learning to understand a foreign language. Someone teaches you the grammar rules so you can read and understand sentences in that language.
  - Use Cases: Expression parsers, query languages, configuration file parsers, rule engines, SQL parsers
  - Description: Defines a grammatical representation for a language and an interpreter to process sentences in that language.
  - Example:
    ```
    interface Expression {
      interpret(context)
    }
    
    class Number implements Expression {
      constructor(value) { this.value = value }
      interpret(context) { return this.value }
    }
    
    class Plus implements Expression {
      constructor(left, right) {
        this.left = left
        this.right = right
      }
      interpret(context) {
        return this.left.interpret(context) + this.right.interpret(context)
      }
    }
    
    class Multiply implements Expression {
      constructor(left, right) {
        this.left = left
        this.right = right
      }
      interpret(context) {
        return this.left.interpret(context) * this.right.interpret(context)
      }
    }
    
    // Usage: (3 + 4) * 2 = 14
    expr = new Multiply(
      new Plus(new Number(3), new Number(4)),
      new Number(2)
    )
    result = expr.interpret(null)
    print(result)  // 14
    ```

#### Iterator
  - ELI5: A cookbook is a collection of recipes. You use a bookmark to go through recipes one by one without needing to know how the book is organized inside.
  - Use Cases: Collection traversal, tree traversal, different iteration strategies, lazy evaluation, cursor-based access
  - Description: Provides a way to access elements of a collection sequentially without exposing its underlying representation.
  - Example:
    ```
    interface Iterator {
      hasNext()
      next()
    }
    
    interface Collection {
      createIterator()
    }
    
    class ListIterator implements Iterator {
      private list
      private index = 0
      
      constructor(list) { this.list = list }
      
      hasNext() { return this.index < this.list.length }
      
      next() {
        if (this.hasNext()) {
          return this.list[this.index++]
        }
        return null
      }
    }
    
    class MyList implements Collection {
      private items = []
      
      add(item) { this.items.push(item) }
      
      createIterator() {
        return new ListIterator(this.items)
      }
    }
    
    // Usage
    list = new MyList()
    list.add("apple")
    list.add("banana")
    list.add("cherry")
    
    iterator = list.createIterator()
    while (iterator.hasNext()) {
      print(iterator.next())
    }
    ```
  
#### Mediator
  - ELI5: A chatroom is a mediator. People in the chatroom don't message each other directly; they send messages to the chatroom which broadcasts them.
  - Use Cases: Chat applications, air traffic control, GUI component coordination, event aggregation, dialog boxes
  - Description: Defines an object that encapsulates how a set of objects interact, promoting loose coupling by preventing objects from referring to each other explicitly.
  - Example:
    ```
    interface ChatRoomMediator {
      sendMessage(msg, from, to)
      registerUser(user)
    }
    
    class ConcreteChatRoom implements ChatRoomMediator {
      private users = {}
      
      registerUser(user) {
        this.users[user.name] = user
        user.setChatRoom(this)
      }
      
      sendMessage(msg, from, to) {
        recipient = this.users[to]
        if (recipient != null) {
          return `${from} to ${to}: ${msg}`
        }
        return "User not found"
      }
    }
    
    class User {
      private chatRoom: ChatRoomMediator
      
      constructor(name) { this.name = name }
      
      setChatRoom(room) { this.chatRoom = room }
      
      send(to, msg) { return this.chatRoom.sendMessage(msg, this.name, to) }
    }
    
    // Usage
    room = new ConcreteChatRoom()
    alice = new User("Alice")
    bob = new User("Bob")
    room.registerUser(alice)
    room.registerUser(bob)
    print(alice.send("Bob", "Hello!"))
    ```

#### Memento
  - ELI5: Saving your game progress. You capture the exact state of the game so you can load it back later and continue from that point.
  - Use Cases: Undo/redo systems, save game functionality, transaction rollback, snapshot storage, change history
  - Description: Captures and externalizes an object's internal state without violating encapsulation, allowing restoration to that state later.
  - Example:
    ```
    class Memento {
      constructor(state) { this.state = state }
      getState() { return this.state }
    }
    
    class GameState {
      constructor(level, health, score) {
        this.level = level
        this.health = health
        this.score = score
      }
    }
    
    class Game {
      private state
      
      constructor() { this.state = new GameState(1, 100, 0) }
      
      play() {
        this.state.level = 2
        this.state.health = 80
        this.state.score = 150
      }
      
      saveState() {
        return new Memento(this.state)
      }
      
      restoreState(memento) {
        this.state = memento.getState()
      }
      
      displayState() {
        return `Level: ${this.state.level}, Health: ${this.state.health}, Score: ${this.state.score}`
      }
    }
    
    // Usage
    game = new Game()
    saved = game.saveState()
    game.play()
    print(game.displayState())  // Level: 2, Health: 80, Score: 150
    game.restoreState(saved)
    print(game.displayState())  // Level: 1, Health: 100, Score: 0
    ```

#### Observer
  - ELI5: A news channel notifies all subscribers whenever there's breaking news. You don't have to keep checking the channel; it tells you automatically.
  - Use Cases: Event listeners, MVC architectures, property change notifications, pub/sub systems, reactive programming
  - Description: Defines a one-to-many dependency between objects where when one object changes state, all dependents are notified automatically.
  - Example:
    ```
    interface Observer {
      update(subject)
    }
    
    class Subject {
      private observers = []
      private state
      
      attach(observer) { this.observers.push(observer) }
      
      detach(observer) {
        index = this.observers.indexOf(observer)
        if (index > -1) this.observers.splice(index, 1)
      }
      
      notify() {
        this.observers.forEach(observer => observer.update(this))
      }
      
      setState(state) {
        this.state = state
        this.notify()
      }
      
      getState() { return this.state }
    }
    
    class ConcreteObserver implements Observer {
      constructor(name) { this.name = name }
      
      update(subject) {
        return `${this.name} received update: ${subject.getState()}`
      }
    }
    
    // Usage
    subject = new Subject()
    observer1 = new ConcreteObserver("Observer 1")
    observer2 = new ConcreteObserver("Observer 2")
    subject.attach(observer1)
    subject.attach(observer2)
    subject.setState("New State")
    ```

#### State
  - ELI5: A traffic light is red, yellow, or green. It behaves differently based on its state. When it's red, it stops cars. When it's green, it lets them go.
  - Use Cases: State machines, workflow engines, game states, document states, connection states
  - Description: Allows an object to alter its behavior when its internal state changes, appearing to change its class.
  - Example:
    ```
    interface State {
      handle(context)
    }
    
    class RedState implements State {
      handle(context) {
        print("Red light: Stop")
        context.setState(new GreenState())
      }
    }
    
    class GreenState implements State {
      handle(context) {
        print("Green light: Go")
        context.setState(new YellowState())
      }
    }
    
    class YellowState implements State {
      handle(context) {
        print("Yellow light: Prepare to stop")
        context.setState(new RedState())
      }
    }
    
    class TrafficLight {
      private state = new RedState()
      
      setState(state) { this.state = state }
      
      request() { this.state.handle(this) }
    }
    
    // Usage
    light = new TrafficLight()
    light.request()  // Red light: Stop
    light.request()  // Green light: Go
    light.request()  // Yellow light: Prepare to stop
    light.request()  // Red light: Stop
    ```

#### Strategy
  - ELI5: Different ways to get to work: drive, take a bus, or bike. You choose the best strategy (method) for today based on the weather.
  - Use Cases: Payment methods, sorting algorithms, compression algorithms, route planning, authentication methods
  - Description: The object has an interface available which is then moved to the instantiating party.
  - Example:
    ```
    interface PaymentStrategy {
      pay(amount)
    }
    
    class CreditCardPayment implements PaymentStrategy {
      constructor(cardNumber) { this.cardNumber = cardNumber }
      pay(amount) { return `Paid ${amount} via credit card ${this.cardNumber}` }
    }
    
    class PayPalPayment implements PaymentStrategy {
      constructor(email) { this.email = email }
      pay(amount) { return `Paid ${amount} via PayPal ${this.email}` }
    }
    
    class ApplePayPayment implements PaymentStrategy {
      pay(amount) { return `Paid ${amount} via Apple Pay` }
    }
    
    class ShoppingCart {
      private items = []
      private paymentStrategy
      
      setPaymentStrategy(strategy) { this.paymentStrategy = strategy }
      
      addItem(item) { this.items.push(item) }
      
      checkout() {
        total = this.items.length * 10  // simplified
        return this.paymentStrategy.pay(total)
      }
    }
    
    // Usage
    cart = new ShoppingCart()
    cart.addItem("Book")
    cart.addItem("Pen")
    cart.setPaymentStrategy(new CreditCardPayment("1234-5678"))
    print(cart.checkout())
    ```

#### Template
  - ELI5: A recipe has steps: mix ingredients, add spices, bake. Different recipes follow this structure but change the specific details (what ingredients, what spices, how long to bake).
  - Use Cases: Framework base classes, document generation, data processing pipelines, algorithm patterns, database access patterns
  - Description: Defines the skeleton of an algorithm in a base class, letting subclasses override specific steps without changing the algorithm's structure.
  - Example:
    ```
    abstract class Beverage {
      // Template method
      prepare() {
        this.boilWater()
        this.brew()
        this.pourInCup()
        this.addCondiments()
      }
      
      boilWater() { print("Boiling water...") }
      pourInCup() { print("Pouring into cup...") }
      
      // Abstract methods - subclasses must implement
      abstract brew()
      abstract addCondiments()
    }
    
    class Coffee extends Beverage {
      brew() { print("Dripping coffee through filter") }
      addCondiments() { print("Adding sugar and milk") }
    }
    
    class Tea extends Beverage {
      brew() { print("Steeping tea bag") }
      addCondiments() { print("Adding lemon and honey") }
    }
    
    // Usage
    coffee = new Coffee()
    coffee.prepare()
    // Output: Boiling water, Dripping coffee through filter, Pouring into cup, Adding sugar and milk
    
    tea = new Tea()
    tea.prepare()
    // Output: Boiling water, Steeping tea bag, Pouring into cup, Adding lemon and honey
    ```

#### Visitor
  - ELI5: A tax inspector visits different types of buildings (houses, shops, factories) and calculates taxes differently for each. The buildings don't change, but the visitor brings new operations.
  - Use Cases: Compiler AST traversal, report generation, serialization/deserialization, optimization passes, document processing
  - Description: Represents an operation to be performed on elements of an object structure, allowing definition of new operations without changing the classes.
  - Example:
    ```
    interface Element {
      accept(visitor: Visitor)
    }
    
    interface Visitor {
      visitHouse(house)
      visitShop(shop)
      visitFactory(factory)
    }
    
    class House implements Element {
      constructor(size) { this.size = size }
      accept(visitor) { return visitor.visitHouse(this) }
    }
    
    class Shop implements Element {
      constructor(size, employees) {
        this.size = size
        this.employees = employees
      }
      accept(visitor) { return visitor.visitShop(this) }
    }
    
    class TaxCalculator implements Visitor {
      visitHouse(h) { return h.size * 100 }
      visitShop(s) { return s.size * 150 + s.employees * 50 }
      visitFactory(f) { return f.size * 200 }
    }
    
    class ReportGenerator implements Visitor {
      visitHouse(h) { return `House with ${h.size} sqm` }
      visitShop(s) { return `Shop with ${s.size} sqm and ${s.employees} employees` }
      visitFactory(f) { return `Factory with ${f.size} sqm` }
    }
    
    // Usage
    elements = [new House(100), new Shop(200, 5)]
    taxCalc = new TaxCalculator()
    totalTax = 0
    for (element in elements) {
      totalTax += element.accept(taxCalc)
    }
    print("Total tax: " + totalTax)
    ```


## Architectural Patterns (Gang of Five)
Architectural patterns describe how entire systems are structured, not just individual classes.

### Modeling
- Break down the system into subsystems and responsibilities.
- Define interfaces among subsystems.
- Define how data moves and where it is owned.

#### Model Characteristics
- Unambiguous: the model should not be open to multiple interpretations.
- Accurate and precise: enough detail to build and validate against requirements.
- Views and viewpoints: represent concerns of different stakeholders.
- Consistent: terms, relationships, and constraints align across diagrams/docs.

#### Architectural Viewpoints
- Logical view: core domain objects and business rules.
- Process view: runtime behavior, concurrency, scaling.
- Development view: modules, services, and code ownership.
- Physical view: deployment topology and infrastructure.
- Scenario view: end-to-end flows that validate the architecture.

### Integration Styles
#### Remote Procedure
- ELI5: One program calls another program's function as if it were local.
- Description: Synchronous request/response style (for example RPC/gRPC) for direct service calls.
- Example:
  ```
  // Service A
  response = UserService.GetUser({ userId: 42 })
  if (response.ok) {
    showProfile(response.user)
  }
  
  // Service B
  GetUser(request) {
    return { ok: true, user: db.findUser(request.userId) }
  }
  ```
- Use Cases: Internal microservice APIs, low-latency service calls, strongly typed contracts.

#### Messaging
- ELI5: Leave a note in a mailbox; the receiver reads it later.
- Description: Asynchronous communication via queues/topics, decoupling producer and consumer.
- Example:
  ```
  // Producer
  bus.publish("order.created", { orderId: 1001, total: 49.99 })

  // Consumer
  bus.subscribe("order.created", (event) => {
    inventory.reserve(event.orderId)
    email.sendConfirmation(event.orderId)
  })
  ```
- Use Cases: Event-driven systems, integrations, retries, burst smoothing.

#### Shared Database
- ELI5: Multiple apps write in the same notebook.
- Description: Multiple services/apps read and write to a common schema and datastore.
- Example:
  ```
  // Billing service
  INSERT INTO orders(id, status, total) VALUES (1001, "PENDING", 49.99)

  // Shipping service
  SELECT status FROM orders WHERE id = 1001
  UPDATE orders SET status = "SHIPPED" WHERE id = 1001
  ```
- Use Cases: Legacy modernization phases, small teams, rapid prototypes.

#### File Transfer
- ELI5: Drop a file in a shared folder for another team to pick up.
- Description: Systems exchange data via files (CSV/JSON/XML), often in scheduled jobs.
- Example:
  ```
  // Producer writes nightly export
  writeFile("/drop/orders_2026_06_19.csv", orders)

  // Consumer imports every hour
  files = readPendingFiles("/drop")
  for (file in files) {
    rows = parseCsv(file)
    importRows(rows)
    archive(file)
  }
  ```
- Use Cases: B2B integrations, batch pipelines, partner onboarding.

### Categories
#### Call and Return
- Object Orientation (OO): collaborate via objects and method calls.
- Layers: split by responsibility (UI, application, domain, infrastructure).
- Client-Server: clients request services from centralized servers.
- Three-Tier: presentation, business logic, and data tiers.

#### Data Flow
- Batch Sequential: one stage completes before next starts.
- Pipes and Filters: data stream passes through independent processors.

#### Shared Memory
- Blackboard: multiple specialists write/read a shared knowledge model.
- Repository: centralized data store accessed through a consistent abstraction.

#### Distributed
- Proxy: local stand-in for remote object/service.
- Broker: intermediary routes requests/events between distributed components.

#### Interactive Systems
- Model-View-Controller (MVC): separates state, presentation, and user input flow.
- Presentation Abstraction Controller (PAC): hierarchical agents with presentation/abstraction/control.

#### Implicit Invocation
- Publish-Subscribe: publishers emit events, subscribers react.
- Request-Reply: sender expects a correlated response message.
- Guaranteed Delivery: messaging ensures no loss via persistence/retry.

#### Concurrency
- Reactor: event loop dispatches handlers for I/O/events.
- Active Object: method calls become queued requests with internal worker threads.

### Common Architectural Patterns
#### Layers
- ELI5: Build a sandwich where each layer has a job.
- Description: Organizes system by responsibility with controlled dependencies.
- Example:
  ```
  Controller -> ApplicationService -> DomainService -> Repository
  ```
- Use Cases: Enterprise apps, maintainable monoliths, clear team boundaries.

#### Client-Server
- ELI5: A customer asks a waiter, and the kitchen serves the dish.
- Description: Clients request resources/services from one or more servers.
- Example:
  ```
  client.request("GET /products")
  server.handle("GET /products") -> db.query("SELECT * FROM products")
  ```
- Use Cases: Web apps, mobile backends, API platforms.

#### Pipes and Filters
- ELI5: Water flows through filters, each filter cleans one thing.
- Description: Data passes through independent processing steps.
- Example:
  ```
  rawLogs -> parseFilter -> enrichFilter -> redactFilter -> storeFilter
  ```
- Use Cases: ETL, log processing, media processing pipelines.

#### Repository
- ELI5: One library desk where everyone borrows books the same way.
- Description: Centralizes data access behind a collection-like interface.
- Example:
  ```
  userRepo.save(user)
  user = userRepo.findByEmail("a@b.com")
  ```
- Use Cases: Domain-driven design, testable data access, ORM abstraction.

#### Broker
- ELI5: A travel agent coordinates flights and hotels so you do not call everyone yourself.
- Description: Middleware routes requests among distributed services.
- Example:
  ```
  serviceA -> broker -> serviceB
  broker.lookup("payment-service")
  broker.forward(request)
  ```
- Use Cases: Service discovery, distributed object systems, integration hubs.

#### MVC
- ELI5: Model is the data, view is what users see, controller is what handles clicks.
- Description: Separates concerns for UI systems.
- Example:
  ```
  Controller.onAddItem() {
    model.addItem(input.value)
    view.render(model.items)
  }
  ```
- Use Cases: Web apps, desktop apps, interactive dashboards.

#### Publish-Subscribe
- ELI5: Subscribe to a channel and get notified automatically when news arrives.
- Description: Event producers and consumers are decoupled by topics.
- Example:
  ```
  eventBus.subscribe("invoice.paid", AccountingHandler)
  eventBus.publish("invoice.paid", { invoiceId: 55 })
  ```
- Use Cases: Event-driven microservices, plugin ecosystems, audit/event streams.

#### Reactor
- ELI5: One receptionist watches many doors and dispatches requests fast.
- Description: Single event loop demultiplexes events to handlers.
- Example:
  ```
  while (true) {
    events = selector.wait()
    for (event in events) {
      handlers[event.type](event)
    }
  }
  ```
- Use Cases: High-concurrency network servers, gateways, proxies.

## Software Design Patterns
Software design patterns are implementation-level patterns commonly used in application code.

#### Dependency Injection
- ELI5: Instead of a class cooking its own food, someone hands it a ready meal.
- Description: Dependencies are provided from the outside instead of created internally.
- Example:
  ```
  interface PaymentGateway {
    charge(amount)
  }

  class StripeGateway implements PaymentGateway {
    charge(amount) { return `charged ${amount}` }
  }

  class CheckoutService {
    constructor(gateway: PaymentGateway) {
      this.gateway = gateway
    }

    checkout(total) {
      return this.gateway.charge(total)
    }
  }

  // Composition root
  gateway = new StripeGateway()
  checkout = new CheckoutService(gateway)
  checkout.checkout(49.99)
  ```
- Use Cases: Testability with mocks, loose coupling, swapping implementations by environment.
