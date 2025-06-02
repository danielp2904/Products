// lib/screens/create_product_screen.dart
import 'package:flutter/material.dart';
import '../services/product_service.dart';

class CreateProductScreen extends StatelessWidget {
  final _formKey = GlobalKey<FormState>();
  final _descController = TextEditingController();
  final _valueController = TextEditingController();
  final ProductService _productService = ProductService();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Novo Produto")),
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
                  await _productService.createProduct(
                    _descController.text,
                    double.parse(_valueController.text),
                  );
                  Navigator.pop(context);
                }
              },
              child: Text("Criar"),
            )
          ]),
        ),
      ),
    );
  }
}
