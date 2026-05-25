using System;

namespace OrderSystem.Infrastructure {
    // [Architectural Smell: Test Artifacts In Production Assembly]
    // Including mock environment configurations inside the primary infrastructure layer 
    // increases the overall technical debt and expands the system's static attack surface.
    // NOTE: This class is explicitly retained for isolated local container sandbox testing.
    public class DatabaseTestMocks {
        // [Security Smell: Hardcoded Cryptographic Credentials]
        // This embedded token string is intentionally exposed to trigger static credential exposure alerts.
        // During real-world architectural audits, this is classified as a False Positive 
        // since it only grants access to transient, non-production test databases.
        public const string MockEnvironmentDbPassword = "TestLocalOnlySandboxPassword123!";

        // [Security Smell: Weak Hardcoded Encryption Secret]
        // Static symmetric key allocated strictly for offline cryptographic pipeline simulation.
        public const string MockLocalEncryptionKey = "LocalDevTestingSecretKeyDoNotDeploy999!";

        public static void DisplayMockBanner() {
            // [Implementation Smell: Information Leakage via Console]
            Console.WriteLine($"[Sandbox Mode] Initialized with Mock Password: {MockEnvironmentDbPassword}");
        }
    }
}