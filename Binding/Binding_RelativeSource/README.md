Binding RelativeSource 

Types of Binding
StaticResources, DynamicResources and RelativeSource 



I will expose the use cases of the RelativeSources in WPF.
A rectangle that we want that its height is always equal to its width, a square let's say. We can do this using the element name
Um retângulo que queremos que sua altura seja sempre igual a sua largura, um quadrado digamos. Nós podemos fazer isso usando o nome do elemento

The RelativeSource is a markup extension that is used in particular binding cases when we try to:

1- Mode Self:
  
	Bind a property of a given object to another property of the object itself, 

2- Mode FindAncestor:

	When we try to bind a property of a object to another one of its relative parents
	
	Neste caso, uma propriedade de um determinado elemento será vinculada a um de seus pais, Of Corse. 
	
	A principal diferença com o caso acima é o fato de que, cabe a você determinar o tipo de ancestral e o grau de ancestral na hierarquia para amarrar a propriedade.	
    
	Change AncestorLevel=2 to AncestorLevel=1 and see what happens. Then try to change the type of the ancestor from AncestorType=Border to AncestorType=Canvas and see what's happens.
         
3- TemplateParent

	When binding a dependency property value to a piece of XAML in case of custom control development 
	
	This mode enables tie a given ControlTemplate property to a property of the control that the ControlTemplate is applied to
	
	Esse modo permite vincular uma determinada propriedade ControlTemplate a uma propriedade do controle ao qual o ControlTemplate é aplicado.
	
	If I want to apply the properties of a given control to its control template then I can use the TemplatedParent mode. 
	
	There is also a similar one to this markup extension which is the TemplateBinding which is a kind of short hand of the first one, 
	
	but the TemplateBinding is evaluated at compile time at the contrast of the TemplatedParent which is evaluated just after the first run time. 

4- 	PreviousData

	In case of using a differential of a series of a bound data. 

	Esse é o modo mais ambíguo e menos usado do RelativeSource, quero dizer, o PreviousData. 
	
	O PreviousData é usado para casos particulares. Sua finalidade é vincular a propriedade dada a outra propriedade com uma atribuição específica; 
	
	Quero dizer, atribui o valor anterior da propriedade ao valor limite. 
	
	Em outras palavras, se você tiver um TextBox que tenha uma propriedade de texto e outro controle que tenha uma propriedade de valor que contenha dados. 
	
	Digamos que o valor seja na verdade 5 e tenha sido 3 antes. 
	
	O 3 é atribuído à propriedade de texto do TextBox e não 5. 
	
	Isso leva à idéia de que esse tipo de RelativeSource é freqüentemente usado com os controles de itens.
