// lib/services/product_service.dart
import 'dart:convert';
import 'package:http/http.dart' as http;

const String baseUrl = "http://192.168.1.3:8080/api/product"; // Altere para o IP real no celular

class ProductService {
  Future<List<dynamic>> getAllProducts() async {
    final response = await http.get(Uri.parse(baseUrl));
    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception("Erro ao buscar produtos");
    }
  }

  Future<void> createProduct(String description, double value) async {
    final response = await http.post(
      Uri.parse(baseUrl),
      headers: {"Content-Type": "application/json"},
      body: jsonEncode({"description": description, "value": value}),
    );

    if (response.statusCode != 200) {
      throw Exception("Erro ao criar produto");
    }
  }

  Future<void> updateProduct(int id, String description, double value) async {
    final response = await http.put(
      Uri.parse("$baseUrl/$id"),
      headers: {"Content-Type": "application/json"},
      body: jsonEncode({"description": description, "value": value}),
    );

    if (response.statusCode != 200) {
      throw Exception("Erro ao atualizar produto");
    }
  }

  Future<void> deleteProduct(int id) async {
    final response = await http.delete(Uri.parse("$baseUrl/$id"));
    if (response.statusCode != 200) {
      throw Exception("Erro ao deletar produto");
    }
  }
}
