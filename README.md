### Projet de session - Phase 1 

### Équipe 12 - Groupe Vert

<br />

## **Rapport**

### 5 exemples dans le code qui démontrent l'application de chacun des 5 principes SOLID : 

<br />

#### 1. Exemple portant sur le *Principe de responsabilité unique* :

```c#
public class Board
    {
        protected const int GridSize = 9;
        protected const int BoxSize = 3;
        public Tile[][] grid { get; set; }
        protected GridStateChecker stateChecker;
        ...
    }
```

#### Explication et justication : 

La classe ```Board``` n'a qu'une seule fonction, gérer le board. C'est-à-dire que c'est cette fonction qui
vérifie si les lignes, les rangées et les boîtes sont correctes en terme de *Sudoku*. 

Théoriquement, le *Principe de responsabilité unique* dicte qu'une classe ne devrait avoir qu'une préoccupation unique 
et que ses méthodes devraient uniquement supporter cette seule préoccupation.

C'est en effet le cas pour la classe ```Board```, et donc, ce principe est appliqué et respecté.



<br />

#### 2. Exemple portant sur le *Principe ouvert/fermé* :

```c#
abstract class Note
    {
        public int position { get; }
        ...
   }
```

```c#
public class CenterNote : Note
    {
        public List<int> notes { get; set; }
        ...
    }
```

```c#
public class CornerNote : Note
    {
        public int note { get; set; }
        ...
    }
```

#### Explication et justication : 


La classe ```Note``` applique le *Principe ouvert/fermé* puisqu'il a 2 enfants nommés ```CenterNote``` et ```CornerNote```.
Ces 2 classes ont hérité de ```Note```. Cela implique également, que si on voulait un autre type de note, soit un autre enfant, 
on n'aurait pas à modifier ```Note```, ```CenterNote``` ou ```CornerNote```.

Théoriquement, le *Principe ouvert/fermé* nous dit que les classes doivent être fermées à la modification, mais ouverte à l'extension. 
Ces classes devraient donc être construites de façon à pouvoir les faire évoluer par extension, via l'héritage ou la composition.

C'est en effet le cas pour la classe ```Note```, et donc, ce principe est appliqué et respecté.

<br />

#### 3. Exemple portant sur le *Principe de subtitution de Liskov* :

```c#
public abstract class Note
    {
        public int position { get; }

        public Note(int position)
        {
            this.position = position;
        }

        public Note(string strToNote)
        {
            position = int.Parse(strToNote.Split("$$")[0]);
        }

        public abstract override string ToString();

        public abstract string ToDisplay();
    }
```

```c#
public class CenterNote : Note
    {
        public List<int> notes { get; set; }

        public CenterNote(int position, int[] notes) : base(position)
        {
            this.notes = notes.ToList();
        }

        public CenterNote(string strToNote) : base(strToNote)
        {
            string[] notesArray = strToNote.Split("$$")[1].Split("**");
            notes = new();
            
            foreach(string str in notesArray)
            {
                this.notes.Add(int.Parse(str));
            }   
        }
        ...
    }
```

```c#
public class CornerNote : Note
    {
        public int note { get; set; }

        public CornerNote(int position, int note) : base(position)
        {
            this.note = note;
        }

        public CornerNote(string strToNote) : base(strToNote)
        {
            this.note = int.Parse(strToNote.Split("$$")[1]);
        }

        public override string ToString()
        {
            return $"{position}$${note}";
        }
        ...
    }
```

#### Explication et justication : 

Les enfants de ```Note```, soit ```CenterNote``` et ```CornerNote```, héritent des caractéristiques du parent, et donc, pourrait le remplacer sans problème tout en 
changeant le comportement. En effet, on peut voir dans le code ci-dessus que ```CenterNote``` est en fait un liste de chiffre et que ```CornerNote``` et juste 1 chiffre.
Les 2 classes n'ont en fait pas le même comportement, mais ces 2 classes sont tout à fait utilisables.

Théoriquement, le *Principe de subtitution de Liskov* dit qu'on doit pouvoir remplacer A par un sous-type de A sans risquer de briser le code. 

C'est en effet le cas pour les classes ```NoteCenter``` et ```NoteCorner```, et donc, ce principe est appliqué et respecté.

<br />

#### 4. Exemple portant sur le *Principe de ségrégation des interfaces* :

```Aucun exemple de code ne peut être fourni provenant de notre projet en lien avec ce principe.```

#### Explication et justication : 

Premièrement, nous n'avons pas eu besoin d'utiliser d'interface dans le cadre de cette phase du projet.

Par contre, nous comprenons le *Principe de ségrégation des interfaces*,
qui se résume à utiliser plusieurs petites interfaces qui ont un rôle précis plutôt qu'une seule grande interface qui gère toutes les fonctionnalités. 

En effet, cette interface est inefficace, car toutes ces méthodes sont inutilisables simultanément, donc il y aurait un manque d'efficience. 

Dans un sens, nous avons respecté ce principe puisque nous n'avons pas utilisé une grande interface qui possède toutes les méthodes.

<br />

#### 5. Exemple portant sur le *Principe d'injection de dépendances* :

```Aucun exemple de code ne peut être fourni provenant de notre projet en lien avec ce principe.```

#### Explication et justication : 

Nous n'avons pas utiliser le *Principe d'injection de dépendances* dans le cadre de cette phase du projet.

Théoriquement, ce principe dit que la dépendance devrait être décidée par l'utilisateur de classes, et non par ces classes elles-mêmes. 

Dans notre projet, nous n'avons pas eu recours à ce genre d'utilisateur de classes, et donc, ce principe n'est pas présent dans cette phase du projet.

<br />
<br />


### 3 exemples dans le code qui démontrent l'application de chacun un principe GRASP différent :

<br />

#### 1. Exemple portant sur le patron *Spécialiste de l'information* :

```c#
public class Board
    {
        protected const int GridSize = 9;
        protected const int BoxSize = 3;
        public Tile[][] grid { get; set; }
        protected GridStateChecker stateChecker;

        public Board()
        {
            grid = new Tile[GridSize][];
            stateChecker = new GridStateChecker(grid, GridSize, BoxSize);

            //Initialise la grille
            for(int ctr = 0; ctr < GridSize; ctr++)
            {
                grid[ctr] = new Tile[GridSize];
            }

            for(int ctrRow = 0; ctrRow < GridSize; ctrRow++)
            {
                for(int ctrCol = 0; ctrCol < GridSize; ctrCol++)
                {
                    grid[ctrRow][ctrCol] = new Tile();
                }
            }
        }
        ...
    }    
```

#### Explication et justication : 

Dans notre cas, la classe ```Board``` est le *Spécialiste de l'information*, puisque c'est cette classe qui a les informations par rapport
aux cases. Cela implique aussi que c'est ```Board``` qui détermine si les cases sont valides. Les autre classes ne pourraient pas comparer les cases sur
l'entièreté de la grille, car c'est seulement la classe ```Board``` qui a ces informations.

Théoriquement, le *Spécialiste de l'information* propose de donner la responsabilité à la classe qui connait les informations.
C'est aussi le patron le plus utilisé pour affecter des responsabilités.

C'est en effet le cas pour la classe ```Board```, et donc, ce patron est appliqué et respecté.

<br />

#### 2. Exemple portant sur le patron *Créateur* :

```c#
public class Board
    {
        protected const int GridSize = 9;
        protected const int BoxSize = 3;
        public Tile[][] grid { get; set; }
        protected GridStateChecker stateChecker;

        public Board()
        {
            grid = new Tile[GridSize][];
            stateChecker = new GridStateChecker(grid, GridSize, BoxSize);

            //Initialise la grille
            for(int ctr = 0; ctr < GridSize; ctr++)
            {
                grid[ctr] = new Tile[GridSize];
            }

            for(int ctrRow = 0; ctrRow < GridSize; ctrRow++)
            {
                for(int ctrCol = 0; ctrCol < GridSize; ctrCol++)
                {
                    grid[ctrRow][ctrCol] = new Tile();
                }
            }
        }
        ...
    }    
```

#### Explication et justication : 

Selon le patron *Créateur*, c'est la classe ```Board``` qui devrait avoir la responsabilité de créer les cases puisque cette classe est composée de ces mêmes cases.
En d'autres mots, c'est la classe ```Board``` qui possède les informations permettant d'initialiser les instances des cases.

Théoriquement, le créateur prend la responsabilité de créer une instance de classe. Aussi, le patron affecte à une classe une responsabilité de créations d'instances.

C'est en effet le cas pour la classe ```Board```, et donc, ce patron est appliqué et respecté.

<br />

#### 3. Exemple portant sur le patron *Controleur* :

```c#
public class BoardController
    {
        public MainWindow view { get; }
        public Board board { get; private set; }
        private GridStateChecker stateChecker;
        public BoardController(MainWindow view)
        {
            this.view = view;
            this.board = new Board();
            this.stateChecker = new GridStateChecker(this.board.grid, this.board.GetGridSize(), this.board.GetBoxSize());
            view.DrawGrid(board.grid);
        }
        ...
    }
```

#### Explication et justication : 

Comme son nom l'indique plutôt bien, la classe ```BoardController``` coordonne les messages provenant de l'interface grapique. Cette classe a donc comme responsabilité 
de contrôler les entrées qui proviennent de l'interface graphique et de transmettre ces messages vers l'intérieur. 

Théoriquement, ce patron permet de maintenir le système objet isolé du monde extérieur, et donc, le contrôleur fait le lien entre le modèle et la vue.

C'est en effet le cas pour la classe ```BoardController```, et donc, ce patron est appliqué et respecté.

<br />
