Priority of Source VS DataContext

PersonWindow Example:

The first TextBox inherits the DataContext from the parent Grid as well as has a Source set in the Binding object too. 
In this case, Source takes priority, causing the TextBox to bind to the Name property of the resource with key “PersonXAMLDataSource” – 
this displays “Osvaldo Martini”. 
The second inherits the DataContext from the parent Grid causing the TextBox to bind to the Name property of the resource with key “Person1”, 
causing it to display “Person_1_Name”. 
WPF will search up the element tree until it encounters a DataContext object if a Source or RelativeSource is not used. 
Once it finds a non-null DataContext, that object is used for binding. It is useful for binding several properties to the same object.

Most data bound applications tend to use DataContext much more heavily than Source. Use DataContext only when you need to bind more than one property to a particular source. 
When binding only one property to a source, always use Source. The reason for this is ease of debugging – We can see all the information about the Binding in one place, than search 
for the nearest DataContext to understand what is going on. Other than setting the DataContext property on an element directly, inheriting the DataContext value from an ancestor, 
and explicitly specifying the binding source by setting the Source property on the Binding, you can also use the ElementName property or the RelativeSource property to specify the binding source.
The ElementName property is useful when you are binding to other elements in your application, such as when you are using a slider to adjust the width of a button. 
The RelativeSource property is useful when the binding is specified in a ControlTemplate or a Style.


Eu tenho uma classe de funcionários e mais uma classe chamada AnotherClass e usei a classe de funcionário como uma propriedade 

dentro dessa classe. Na classe mainwindow, estamos criando um objeto de classe de funcionário e inicializando-o com alguns valores. 

Então, estamos criando um objeto anotherClass e inicializando com o objeto employee criado anteriormente, bem como mais uma propriedade de anotherclass "State" e, 

em seguida, estamos atribuindo esse objeto anotherclass como dataconext. Existem várias maneiras de especificar o objeto de origem de ligação. 

Usar a propriedade DataContext em um elemento pai é útil quando você está vinculando várias propriedades à mesma fonte. 

No entanto, às vezes, pode ser mais apropriado especificar a fonte de ligação em declarações individuais de ligação.

O Primeiro TextBox e Segundo herdam o DataContext do pai Grid, assim como também tem um Source definido no objeto Binding. 

Nesse caso, Source tem prioridade, fazendo com que o TextBox seja vinculado à propriedade Name do recurso com a chave “PersonXAMLDataSource” - isso exibe "Osvaldo Martini...”. 

O Terceiro TextBox herda o DataContext da grade pai fazendo com que o TextBox se associe à propriedade Name do recurso com a chave “Person1”, fazendo com que ele exiba “Person1 Name”. 

O WPF pesquisará a árvore de elementos até encontrar um objeto DataContext se uma Origem ou um RelativeSource não for usado. Depois de encontrar um DataContext não nulo, 

esse objeto é usado para ligação. É útil para ligar várias propriedades ao mesmo objeto.

A maioria dos aplicativos ligados a dados tendem a usar o DataContext muito mais do que o Source. 

Use DataContext somente quando precisar vincular mais de uma propriedade a uma fonte específica. 

Ao vincular apenas uma propriedade a uma fonte, sempre use Source. 

A razão para isso é a facilidade de depuração - podemos ver todas as informações sobre a vinculação em um lugar,  em vez de procurar o DataContext mais próximo para entender o que está acontecendo. 

Além de definir a propriedade DataContext em um elemento diretamente, herdando o valor DataContext de um ancestral e explicitamente especificando a origem da ligação definindo a 

propriedade Source na Associação, você também pode usar a propriedade ElementName ou a propriedade RelativeSource para especificar a origem da ligação . 

A propriedade ElementName é útil quando você está vinculando a outros elementos em seu aplicativo, como quando você está usando um controle deslizante para ajustar a largura de um botão. 

A propriedade RelativeSource é útil quando a ligação é especificada em um ControlTemplate ou em um Style.


No XAML: Embora tenhamos atribuído o objeto anotherclass inteiro como datacontext, estou apenas atribuindo o objeto EmployeeNameTest a ele como o datacontext da grade usando 

o código <Grid DataContext = "{Binding Path = EmployeeNameTest}">.