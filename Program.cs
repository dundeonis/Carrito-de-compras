using System;

class Producto
{
    public int Id;
    public string Nombre;
    public decimal Precio;
    public int Stock;

    public Producto(int id, string nombre, decimal precio, int stock)
    {
        Id = id;
        Nombre = nombre;
        Precio = precio;
        Stock = stock;
    }
}

class Carrito
{
    public Producto[] Productos;
    public int[] Cantidades;

    public Carrito(Producto[] productos)
    {
        Productos = productos;
        Cantidades = new int[productos.Length];
    }

    public bool Agregar(int id, int cantidad)
    {
        for (int i = 0; i < Productos.Length; i++)
        {
            if (Productos[i].Id == id)
            {
                if (Cantidades[i] + cantidad > Productos[i].Stock)
                {
                    return false;
                }
                Cantidades[i] = Cantidades[i] + cantidad;
                return true;
            }
        }
        return false;
    }

    public decimal Total()
    {
        decimal total = 0;
        for (int i = 0; i < Productos.Length; i++)
        {
            total = total + Productos[i].Precio * Cantidades[i];
        }
        return total;
    }

    public bool EstaVacio()
    {
        for (int i = 0; i < Cantidades.Length; i++)
        {
            if (Cantidades[i] > 0)
            {
                return false;
            }
        }
        return true;
    }

    public void Vaciar()
    {
        for (int i = 0; i < Cantidades.Length; i++)
        {
            Cantidades[i] = 0;
        }
    }

    public void Pagar()
    {
        for (int i = 0; i < Productos.Length; i++)
        {
            Productos[i].Stock = Productos[i].Stock - Cantidades[i];
        }
        Vaciar();
    }
}

class Program
{
    static Producto[] productos = new Producto[4];
    static Carrito carrito;

    static void Main()
    {
        productos[0] = new Producto(1, "Laptop", 6500, 5);
        productos[1] = new Producto(2, "Mouse", 120, 25);
        productos[2] = new Producto(3, "Teclado", 450, 10);
        productos[3] = new Producto(4, "Monitor", 1350, 8);
        carrito = new Carrito(productos);

        int opcion = -1;
        while (opcion != 0)
        {
            Console.WriteLine();
            Console.WriteLine("1. Ver catalogo");
            Console.WriteLine("2. Agregar producto");
            Console.WriteLine("3. Ver carrito");
            Console.WriteLine("4. Vaciar carrito");
            Console.WriteLine("5. Pagar");
            Console.WriteLine("0. Salir");
            opcion = LeerNumero("Opcion: ");

            if (opcion == 1)
            {
                VerCatalogo();
            }
            else if (opcion == 2)
            {
                AgregarProducto();
            }
            else if (opcion == 3)
            {
                VerCarrito();
            }
            else if (opcion == 4)
            {
                carrito.Vaciar();
                Console.WriteLine("Carrito vaciado.");
            }
            else if (opcion == 5)
            {
                Pagar();
            }
            else if (opcion != 0)
            {
                Console.WriteLine("Opcion invalida.");
            }
        }
    }

    static int LeerNumero(string mensaje)
    {
        Console.Write(mensaje);
        int numero;
        int.TryParse(Console.ReadLine(), out numero);
        return numero;
    }

    static void VerCatalogo()
    {
        for (int i = 0; i < productos.Length; i++)
        {
            Console.WriteLine(productos[i].Id + ". " + productos[i].Nombre + " - Q" + productos[i].Precio + " (stock: " + productos[i].Stock + ")");
        }
    }

    static void AgregarProducto()
    {
        int id = LeerNumero("ID del producto: ");
        int cantidad = LeerNumero("Cantidad: ");

        if (cantidad <= 0)
        {
            Console.WriteLine("Cantidad invalida.");
        }
        else if (carrito.Agregar(id, cantidad))
        {
            Console.WriteLine("Producto agregado.");
        }
        else
        {
            Console.WriteLine("No existe el producto o no hay stock suficiente.");
        }
    }

    static void VerCarrito()
    {
        if (carrito.EstaVacio())
        {
            Console.WriteLine("El carrito esta vacio.");
            return;
        }

        for (int i = 0; i < productos.Length; i++)
        {
            if (carrito.Cantidades[i] > 0)
            {
                Console.WriteLine(productos[i].Nombre + " x" + carrito.Cantidades[i]);
            }
        }
        Console.WriteLine("Total: Q" + carrito.Total());
    }

    static void Pagar()
    {
        if (carrito.EstaVacio())
        {
            Console.WriteLine("El carrito esta vacio.");
            return;
        }

        decimal total = carrito.Total();
        carrito.Pagar();
        Console.WriteLine("Pago realizado. Total: Q" + total);
    }
}
