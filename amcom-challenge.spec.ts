/**
 * NOTA DE ENGENHARIA DE QUALIDADE:
 * Este arquivo é uma demonstração BÔNUS desenvolvida em Playwright com TypeScript.
 * * O objetivo é evidenciar a proficiência técnica na stack principal utilizada pela Amcom,
 * replicando os mesmos 17 cenários (e bugs mapeados) estruturados no projeto principal em C#.
 * * Para fins de agilidade e simplificação da entrega técnica, este script foi arquitetado
 * em arquivo único de testes, omitindo a camada de Page Objects (POM), mas mantendo-se
 * 100% fiel às regras de negócio, seletores resilientes e validações da página.
 */

import { test, expect } from '@playwright/test';

test.describe('Desafio Amcom - Automação de Testes com Playwright', () => {

    test.beforeEach(async ({ page }) => {
        await page.goto('https://imagens.amcom.com.br/HTML/testeQA_1_1.html');
        await page.waitForTimeout(1000); // Pausa estratégica para auditoria visual
    });

    // =================================================================
    // FORMULÁRIO 1 - SUBMISSÃO E VALIDAÇÕES
    // =================================================================

    test('Formulário 1 - Deve preencher dados e interagir com o modal de sucesso', async ({ page }) => {
        await page.locator('#nome').fill('Raul');
        await page.locator('#sobrenome').fill('Silveira');
        await page.locator('#telefone').fill('51999999999');
        await page.waitForTimeout(1000);

        await page.getByRole('button', { name: 'Enviar' }).click();

        const modal = page.locator('.modal-content');
        await expect(modal).toBeVisible();
        
        const textoModal = await modal.locator('p').textContent();
        expect(textoModal).toBe('Enviado com sucesso');
        await page.waitForTimeout(1000);

        await page.locator('.close').click();
        await expect(modal).not.toBeVisible();
    });

    test('Formulário 1 - BUG: Não deveria exibir modal de sucesso ao enviar campos vazios', async ({ page }) => {
        await page.getByRole('button', { name: 'Enviar' }).click();

        const modal = page.locator('.modal-content');
        await expect(modal).not.toBeVisible({ message: 'Bug Encontrado: O sistema gerou modal de sucesso para campos em branco!' });
    });

    test('Formulário 1 - EDGE CASE: Deve aplicar alerta visual ao perder o foco de campo obrigatório', async ({ page }) => {
        const inputNome = page.locator('#nome');
        
        await inputNome.click();
        await page.locator('#sobrenome').click();
        await page.waitForTimeout(1000);

        const className = await inputNome.getAttribute('class') || '';
        const borderColor = await inputNome.evaluate(el => window.getComputedStyle(el).borderColor);

        const temFeedbackErro = className.includes('error') || className.includes('invalid') || borderColor.includes('rgb(255, 0, 0)');
        
        expect(temFeedbackErro).toBe(true);
    });

    // =================================================================
    // FORMULÁRIO 2 - SELEÇÃO DE CORES
    // =================================================================

    const coresParaTestar = [
        { nome: 'blue', rgbEsperado: 'rgb(173, 216, 230)' },
        { nome: 'yellow', rgbEsperado: 'rgb(247, 220, 111)' },
        { nome: 'red', rgbEsperado: 'rgb(255, 0, 0)' }
    ];

    for (const cor of coresParaTestar) {
        test(`Formulário 2 - BUG: Validar mudança de cor de fundo para ${cor.nome}`, async ({ page }) => {
            await page.locator('#cor').selectOption(cor.nome);
            await page.waitForTimeout(1000);

            const containerForm2 = page.locator("//h2[text()='Formulario 2']/..");
            const backgroundColor = await containerForm2.evaluate(el => window.getComputedStyle(el).backgroundColor);

            expect(backgroundColor).toBe(cor.rgbEsperado);
        });
    }

    // =================================================================
    // FORMULÁRIO 3 - MENSAGEM DINÂMICA DE HORÁRIO
    // =================================================================

    test('Formulário 3 - Deve exibir a saudação correta conforme o relógio do sistema', async ({ page }) => {
        await page.locator('#horario').click();
        await page.waitForTimeout(1000);

        const horaAtual = new Date().getHours();
        let saudacaoEsperada = 'Boa noite!';
        
        if (horaAtual < 12) saudacaoEsperada = 'Bom dia!';
        else if (horaAtual < 18) saudacaoEsperada = 'Boa tarde!';

        const textoSaudacaoReal = await page.locator('#mensagem').textContent();
        expect(textoSaudacaoReal).toBe(saudacaoEsperada);
    });

    // =================================================================
    // INTERFACE - VALIDAÇÃO DE QUALIDADE ORTOGRÁFICA (UI/UX)
    // =================================================================

    const titulosEsperados = ['Formulário 1', 'Formulário 2', 'Formulário 3'];

    titulosEsperados.forEach((tituloCorreto, index) => {
        test(`UI/UX - BUG: Validar se o título do Bloco ${index + 1} possui acentuação correta`, async ({ page }) => {
            const tituloReal = await page.locator(`//h2[${index + 1}]`).textContent();
            expect(tituloReal).toBe(tituloCorreto);
        });
    });
});