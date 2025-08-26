# NFT-Cert-Issue-Check

NFT-Cert-Issue-Check is a .NET 8 solution for minting and verifying NFT-based certificates on the Ethereum blockchain. It leverages Nethereum for blockchain interactions and QRCoder for certificate QR code generation.

## Features

- **Mint NFT Certificates:**  
  Mints NFTs to specified Ethereum addresses and generates certificate images with embedded QR codes linking to transaction details on Etherscan.

- **Verify NFT Ownership:**  
  Provides functionality to check NFT ownership and validity.

- **Smart Contract Integration:**  
  Loads contract ABIs from JSON files and interacts with deployed contracts.

## Technologies

- **C# 12.0 / .NET 8**
- [Nethereum](https://github.com/Nethereum/Nethereum) – Ethereum blockchain integration
- [QRCoder](https://github.com/codebude/QRCoder) – QR code generation
- [Newtonsoft.Json](https://www.newtonsoft.com/json) – JSON parsing

## Project Structure

- `NFTBusiness/services/`
  - `Mint.cs` – Handles NFT minting and certificate generation.
  - `ContractInfo.cs` – Loads contract ABI from JSON files.
  - `CheckNFT.cs` – Verifies NFT ownership.
- `NFTBusiness/interfaces/`
  - `IMint.cs`, `IContractInfo.cs`, `ICheckNFT.cs` – Service interfaces.
- `Controllers/`
  - `MintNFTController.cs`, `CheckNFTController.cs` – API endpoints for minting and checking NFTs.
- `BuiltContract/`
  - Contains contract JSON files (e.g., `FishToken.json`, `GuakGuakToken.json`).

## Getting Started

### Prerequisites

- .NET 8 SDK
- Ethereum testnet account and private key
- Infura endpoint (or other Ethereum node provider)

### Configuration

Update the following in your code or configuration:
- **Infura URL** (e.g., `https://rinkeby.infura.io/v3/<your_project_id>`)
- **Contract addresses** and **ABI file paths**
- **Private key** and **chain ID**

### Building

dotnet build


### Running

You can run the solution as a web API or integrate the services into your application.

### Minting an NFT Certificate

1. Call the `MintCert()` method in `Mint.cs` with the required properties set.
2. The method will:
   - Mint an NFT via the smart contract.
   - Generate a certificate image with a QR code linking to the transaction on Etherscan.

### Verifying an NFT

1. Use the `CheckNFT` service to query ownership and validity.

## Example Usage

var mintService = new Mint(); mintService.owner_address = "<recipient_address>"; mintService.pkey = "<private_key>"; mintService.chainId = 4; // Rinkeby testnet mintService.contract_address = "<contract_address>"; mintService.ABI = "<contract_abi>"; mintService.authority_address = "<authority_address>";
Bitmap certImage = mintService.MintCert(); // Save or display certImage as needed


## License

This project is licensed under the MIT License.

## Acknowledgements

- [Nethereum](https://github.com/Nethereum/Nethereum)
- [QRCoder](https://github.com/codebude/QRCoder)
- [Infura](https://infura.io/)


## Video Demo
[![NFT-Cert-Issue-Check Demo](https://img.youtube.com/vi/your_video_id/0.jpg)](https://www.youtube.com/watch?v=your_video_id)
