using System;

// ================================
// CLASE GENERICA PRINCIPAL (API)
// ==================================
class ApiGenerica<T>
{
    // Estructura de datos: Lista (puedes cambiarla a LinkedList, Queue, Stack...)
    private List<T> items = new List<T>();

    // 1. Método para AGREGAR un item
    public void Agregar(T item)
    {
        items.Add(item);
        Console.WriteLine($" Item agregado correctamente. Total: {items.Count}");
    }

    // 2. Método para LISTAR todos los items
    public void Listar()
    {
        Console.WriteLine($"\n LISTANDO {typeof(T).Name.ToUpper()}S:");
        Console.WriteLine(new string('-', 40));

        if (items.Count == 0)
        {
            Console.WriteLine("  No hay items para mostrar.");
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {items[i]}");
            }
        }
        Console.WriteLine(new string('-', 40));
    }

    // 3. Método para BUSCAR items (por criterio)
    public void Buscar(Func<T, bool> criterio)
    {
        Console.WriteLine($"\n RESULTADOS DE BUSQUEDA:");
        Console.WriteLine(new string('-', 40));

        var resultados = items.Where(criterio).ToList();

        if (resultados.Count == 0)
        {
            Console.WriteLine("  No se encontraron resultados.");
        }
        else
        {
            foreach (var item in resultados)
            {
                Console.WriteLine($"  {item}");
            }
        }
        Console.WriteLine(new string('-', 40));
    }

    // 4. Método para ACTUALIZAR un item
    public void Actualizar(int indice, T nuevoItem)
    {
        if (indice >= 0 && indice < items.Count)
        {
            items[indice] = nuevoItem;
            Console.WriteLine($" Item en posición {indice + 1} actualizado.");
        }
        else
        {
            Console.WriteLine($" Índice {indice + 1} no válido.");
        }
    }

    // 5. Método para ELIMINAR un item
    public void Eliminar(int indice)
    {
        if (indice >= 0 && indice < items.Count)
        {
            items.RemoveAt(indice);
            Console.WriteLine($" Item en posición {indice + 1} eliminado.");
        }
        else
        {
            Console.WriteLine($" Índice {indice + 1} no válido.");
        }
    }

    // 6. Método para CONTAR items
    public int Contar()
    {
        return items.Count;
    }
}

// =============================
// CLASES ESPECÍFICAS (RECURSOS)
// ===============================

// CLASE PRODUCTO (con string, int, float)
class Producto
{
    public string Nombre { get; set; }
    public float Precio { get; set; }
    public int Cantidad { get; set; }

    public Producto(string nombre, float precio, int cantidad)
    {
        Nombre = nombre;
        Precio = precio;
        Cantidad = cantidad;
    }

    // Para mostrar el producto bonito
    public override string ToString()
    {
        return $"Producto: {Nombre} | Precio: ${Precio} | Stock: {Cantidad}";
    }
}

// CLASE CLIENTE
class Cliente
{
    public string Nombre { get; set; }
    public string Email { get; set; }
    public int Telefono { get; set; }

    public Cliente(string nombre, string email, int telefono)
    {
        Nombre = nombre;
        Email = email;
        Telefono = telefono;
    }

    public override string ToString()
    {
        return $"Cliente: {Nombre} | Email: {Email} | Tel: {Telefono}";
    }
}

// CLASE EMPLEADO
class Empleado
{
    public string Nombre { get; set; }
    public string Cargo { get; set; }
    public float Salario { get; set; }

    public Empleado(string nombre, string cargo, float salario)
    {
        Nombre = nombre;
        Cargo = cargo;
        Salario = salario;
    }

    public override string ToString()
    {
        return $"Empleado: {Nombre} | Cargo: {Cargo} | Salario: ${Salario}";
    }
}

// ===========================================
// PROGRAMA PRINCIPAL
// ===========================================
class Program
{
    static void Main()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine(" API GENÉRICA - GESTIÓN DE RECURSOS");
        Console.WriteLine("==========================================\n");

        // Crear instancias de la API para diferentes tipos de recursos
        ApiGenerica<Producto> apiProductos = new ApiGenerica<Producto>();
        ApiGenerica<Cliente> apiClientes = new ApiGenerica<Cliente>();
        ApiGenerica<Empleado> apiEmpleados = new ApiGenerica<Empleado>();

        // ===========================================
        // 1. OPERACIONES CON PRODUCTOS
        // ===========================================
        Console.WriteLine(" GESTIÓN DE PRODUCTOS");
        Console.WriteLine("------------------------");

        apiProductos.Agregar(new Producto("Laptop", 1000.99f, 7));
        apiProductos.Agregar(new Producto("Mouse", 30.50f, 30));
        apiProductos.Agregar(new Producto("Teclado", 43.30f, 18));

        apiProductos.Listar();

        // Buscar productos (precio mayor a 50)
        Console.WriteLine("Buscando productos con precio > $50:");
        apiProductos.Buscar(p => p.Precio > 50);

        // Actualizar un producto
        apiProductos.Actualizar(0, new Producto("Laptop Gaming", 1299.99f, 3));

        // Listar después de actualizar
        apiProductos.Listar();

        Console.WriteLine();

        // ===========================================
        // 2. OPERACIONES CON CLIENTES
        // ===========================================
        Console.WriteLine(" GESTIÓN DE CLIENTES");
        Console.WriteLine("------------------------");

        // Agregar clientes
        apiClientes.Agregar(new Cliente("Juan Pérez", "juan@email.com", 123456789));
        apiClientes.Agregar(new Cliente("María García", "maria@email.com", 987654321));

        // Listar clientes
        apiClientes.Listar();

        // Buscar cliente por nombre
        Console.WriteLine("Buscando cliente 'Juan':");
        apiClientes.Buscar(c => c.Nombre.Contains("Juan"));

        Console.WriteLine();

        // ===========================================
        // 3. OPERACIONES CON EMPLEADOS
        // ===========================================
        Console.WriteLine(" GESTIÓN DE EMPLEADOS");
        Console.WriteLine("------------------------");

        // Agregar empleados
        apiEmpleados.Agregar(new Empleado("Carlos Ruiz", "Programador", 35000f));
        apiEmpleados.Agregar(new Empleado("Ana López", "Diseñadora", 32000f));
        apiEmpleados.Agregar(new Empleado("Pedro Sánchez", "Gerente", 50000f));


        apiEmpleados.Listar();

        // Buscar empleados con salario > 33000
        Console.WriteLine("Empleados con salario > $33000:");
        apiEmpleados.Buscar(e => e.Salario > 33000);


        apiEmpleados.Eliminar(1); // Elimina a Ana López


        apiEmpleados.Listar();

        // Mostrar total de items
        Console.WriteLine("\n ESTADÍSTICAS:");
        Console.WriteLine($"  Productos: {apiProductos.Contar()}");
        Console.WriteLine($"  Clientes: {apiClientes.Contar()}");
        Console.WriteLine($"  Empleados: {apiEmpleados.Contar()}");

        Console.WriteLine("\n PROGRAMA FINALIZADO");
    }
}
