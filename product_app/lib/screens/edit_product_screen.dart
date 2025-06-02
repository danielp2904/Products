// lib/screens/edit_product_screen.dart
import 'package:flutter/material.dart';
import '../services/product_service.dart';

class EditProductScreen extends StatefulWidget {
  final dynamic product;
  EditProductScreen({required this.product});

  @override
  State<EditProductScreen> createState() => _EditProductScreenState();
}

class _EditProductScreenState extends State<EditProductScreen> {
  final _formKey = GlobalKey<FormState>();
  late TextEditingController _descController;
  late TextEditingController _valueController;
  final ProductService _productService = ProductService();

  @override
  void initState() {
    super.initState();
    _descController = TextEditingController(text: widget.product['description']);
    _valueController = TextEditingController(text: widget.product['value'].toString());
  }

  @override
  Widget build(BuildContext context) {
    final id = widget.product['productId'];

    return Scaffold(
      appBar: AppBar(title: Text("Editar Produto")),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Form(
          key: _formKey,
          child: Column(children: [
            TextFormField(
              controller: _descController,
              decoration: InputDecoration(labelText: 'Descrição'),
              validator: (val) => val == null || val.isEmpty ? "Campo obrigatório" : null,
            ),
            TextFormField(
              controller: _valueController,
              decoration: InputDecoration(labelText: 'Valor'),
              keyboardType: TextInputType.number,
              validator: (val) {
                if (val == null || val.isEmpty) return "Campo obrigatório";
                final value = double.tryParse(val);
                return (value == null || value <= 0) ? "Valor inválido" : null;
              },
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: () async {
                if (_formKey.currentState!.validate()) {
                  await _productService.updateProduct(
                    id,
                    _descController.text,
                    double.parse(_valueController.text),
                  );  
                  Navigator.pop(context);
                }
              },
              child: Text("Atualizar"),
            ),
            TextButton(
              onPressed: () async {
                await _productService.deleteProduct(id);
                Navigator.pop(context);
              },
              child: Text("Excluir", style: TextStyle(color: Colors.red)),
            ),
          ]),
        ),
      ),
    );
  }
}
